namespace SocialMedia.Services.Data
{
    using System;
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
            var roomsInitiatedByCurrentUser = await this.chatRoomRepository.All()
                .Where(c => c.UserOneId == userId)
                .Select(c => new AllChatRoomsViewModel
                {
                    Id = c.Id,
                    Username = c.UserTwo.UserName,
                    RecipientFullName = c.UserTwo.FirstName + " " + c.UserTwo.LastName,
                    ProfilePicture = c.UserTwo.ProfilePictureUrl,
                    LastMessage = c.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Text,
                    LastMessageDate = c.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn.ToString("dd MMM"),
                })
                .ToListAsync();

            var roomsInitiatedByAnotherUser = await this.chatRoomRepository.All()
                .Where(c => c.UserTwoId == userId)
                .Select(c => new AllChatRoomsViewModel
                {
                    Id = c.Id,
                    Username = c.UserOne.UserName,
                    RecipientFullName = c.UserOne.FirstName + " " + c.UserOne.LastName,
                    ProfilePicture = c.UserOne.ProfilePictureUrl,
                    LastMessage = c.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Text,
                    LastMessageDate = c.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn.ToString("dd MMM"),
                })
                .ToListAsync();

            var rooms = roomsInitiatedByCurrentUser.Concat(roomsInitiatedByAnotherUser);

            return rooms;
        }

        public async Task<MessageViewModel> CreateAsync(MessageInputModel model)
        {
            var chatRoom = await this.InitiliazeChatRoom(model);

            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.Id == model.UserOneId);

            if (user == null)
            {
                throw new ArgumentNullException("User is invalid.");
            }

            var message = new Message
            {
                ChatRoomId = chatRoom.Id,
                UserId = model.UserOneId,
                Text = model.Text,
                User = user,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();

            var mappedMessage = AutoMapperConfig.MapperInstance.Map<Message, MessageViewModel>(message);

            return mappedMessage;
        }

        public async Task<IEnumerable<MessageViewModel>> GetChatRoom(string currentId, string receiverUsername)
        {
            var receiver = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == receiverUsername);

            if (receiver ==  null)
            {
                throw new ArgumentNullException("User is invalid.");
            }

            var receiverId = receiver.Id;

            var chatRoom = await this.messageRepository.All()
                .Where(c =>
                (c.ChatRoom.UserOneId == currentId && c.ChatRoom.UserTwoId == receiverId) ||
                (c.ChatRoom.UserOneId == receiverId && c.ChatRoom.UserTwoId == currentId))
                .To<MessageViewModel>()
                .ToListAsync();

            return chatRoom;
        }

        private async Task<ChatRoom> InitiliazeChatRoom(MessageInputModel model)
        {
            var userTwo = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == model.UserTwoUsername);

            if (userTwo == null)
            {
                throw new ArgumentNullException("User is invalid.");
            }

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
