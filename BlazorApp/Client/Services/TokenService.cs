using Blazored.LocalStorage;

namespace BlazorApp.Client.Services
{
    public interface ITokenService
    {
        Task<string> GetToken();
        Task RemoveToken();
        Task SetToken(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetToken(string token)
        {
            await localStorageService.SetItemAsync("token", token);
        }

        public async Task<string> GetToken()
        {
            return await localStorageService.GetItemAsync<string>("token");
        }

        public async Task RemoveToken()
        {
            await localStorageService.RemoveItemAsync("token");
        }
    }
}
