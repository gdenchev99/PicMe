﻿namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task<IEnumerable<AllChatRoomsViewModel>> GetAllChatRoomsAsync(string userId);

        Task CreateAsync(MessageInputModel model);

        Task<IEnumerable<MessageViewModel>> GetChatRoom(string username);
    }
}
