namespace SocialMedia.Features.Chat
{
    using Microsoft.AspNetCore.SignalR;

    using System;
    using System.Threading.Tasks;

    public class ChatHub : Hub<IChatClient>
    {
        public async Task DirectMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
