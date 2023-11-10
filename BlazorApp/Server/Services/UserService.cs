using BlazorApp.Server.Data;
using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task RegisterUserAsync(RegisterModel model, CancellationToken cancellationToken = default)
        {
            var existUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username, cancellationToken);
            if (existUser != null)
            {
                throw new ApplicationException("Такой пользователь уже существует");
            }

            await _dataContext.Users.AddAsync(new User
            {
                Name = model.Name,
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
            }, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> LoginUserAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password, cancellationToken);
        }
    }
}
