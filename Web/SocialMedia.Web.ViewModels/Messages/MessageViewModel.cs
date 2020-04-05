namespace SocialMedia.Web.ViewModels.Messages
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ChatRoomId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string Text { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>().ForMember(
                m => m.CreatedOn,
                opt => opt.MapFrom(x => x.CreatedOn.ToString("HH:MM | MMM dd")));
        }
    }
}
