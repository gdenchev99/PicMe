namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ChatRoom> chatRoomRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public MessagesService(
            IRepository<Message> messageRepository,
            IRepository<ChatRoom> chatRoomRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.messageRepository = messageRepository;
            this.chatRoomRepository = chatRoomRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<AllChatRoomsViewModel>> GetAllChatRoomsAsync(string userId)
        {
            var allRooms = await this.chatRoomRepository.All()
                .Where(c => c.Messages.Count > 0)
                .OrderByDescending(c => c.Messages.Count)
                .To<AllChatRoomsViewModel>()
                .ToListAsync();

            return allRooms;
        }

        public async Task CreateAsync(MessageInputModel model)
        {
            var chatRoom = await this.InitiliazeChatRoom(model);

            var message = new Message
            {
                ChatRoomId = chatRoom.Id,
                UserId = model.UserOneId,
                Text = model.Text,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageViewModel>> GetChatRoom(string username)
        {
            var chatRoom = await this.messageRepository.All()
                .Where(c => c.ChatRoom.UserOne.UserName == username || c.ChatRoom.UserTwo.UserName == username)
                .To<MessageViewModel>()
                .ToListAsync();

            return chatRoom;
        }

        private async Task<ChatRoom> InitiliazeChatRoom(MessageInputModel model)
        {
            var userTwo = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == model.UserTwoUsername);
            var userTwoId = userTwo.Id;

            var chatRoomExists = await this.chatRoomRepository.All()
                .FirstOrDefaultAsync(c =>
                (c.UserOneId == model.UserOneId && c.UserTwoId == userTwoId) ||
                (c.UserOneId == userTwoId && c.UserTwoId == model.UserOneId));

            if (chatRoomExists == null)
            {
                var chatRoom = new ChatRoom
                {
                    UserOneId = model.UserOneId,
                    UserTwoId = userTwoId,
                };

                await this.chatRoomRepository.AddAsync(chatRoom);

                await this.chatRoomRepository.SaveChangesAsync();

                return chatRoom;
            }

            return chatRoomExists;
        }
    }
}
