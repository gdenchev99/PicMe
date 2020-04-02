namespace SocialMedia.Features.Chat
{
    using Microsoft.AspNetCore.SignalR;

    using System;
    using System.Threading.Tasks;

    public class ChatHub : Hub<IChatClient>
    {
        public async Task DirectMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(message);
        }
    }
}
