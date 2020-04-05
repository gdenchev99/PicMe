namespace SocialMedia.Features.Chat
{
    using Microsoft.AspNetCore.SignalR;
    using SocialMedia.Services.Data;
    using SocialMedia.Web.ViewModels.Messages;
    using System;
    using System.Threading.Tasks;

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessagesService messageService;

        public ChatHub(IMessagesService messageService)
        {
            this.messageService = messageService;
        }

        public async Task JoinChatRoom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task SendMessage(MessageInputModel model)
        {
            var message = await this.messageService.CreateAsync(model);

            await Clients.Group(message.ChatRoomId.ToString()).ReceiveMessage(message);
        }
    }
}
