using BlazorApp.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Get().Username, message);
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.User.Get().Username;
            if (!string.IsNullOrEmpty(username))
            {
                await Clients.All.SendAsync("Notify", $"{Context.User.Get().Username} вошел в чат");
                await base.OnConnectedAsync();
            }          
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Get().Username} покинул в чат");
            await base.OnDisconnectedAsync(exception);            
        }
    }
}
