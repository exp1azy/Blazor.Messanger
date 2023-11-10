using BlazorApp.Server.Data;
using BlazorApp.Server.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/user")]
    public class HomeController : Controller
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public HomeController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return BadRequest(new ResultModel { Message = "Регистрационные данные не могут быть пустыми" });
            }
            
            try
            {
                await _userService.RegisterUserAsync(model, cancellationToken);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ResultModel { Message = ex.Message });
            }
            
            return Ok(new ResultModel { Message = "Регистрация прошла успешно" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken)
        {
            var existUser = await _userService.LoginUserAsync(model.Username, model.Password, cancellationToken);
            if (existUser == null)
            {
                return Unauthorized(new ResultModel { Message = $"Пользователь {model.Username} не найден" });
            }

            var token = GenerateJwtToken(existUser);

            return Ok(new ResultModel { Message = "Авторизация прошла успешно", Jwt = token });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                jwtSection["Issuer"],
                jwtSection["Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
