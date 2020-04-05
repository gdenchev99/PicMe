namespace SocialMedia.Web.ViewModels.Messages
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class AllChatRoomsViewModel : IMapFrom<ChatRoom>
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string RecipientFullName { get; set; }

        public string ProfilePicture { get; set; }

        public string LastMessage { get; set; }

        public string LastMessageDate { get; set; }
    }
}
