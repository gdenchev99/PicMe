namespace SocialMedia.Web.ViewModels.Messages
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using System.Linq;

    public class AllChatRoomsViewModel : IMapFrom<ChatRoom>, IHaveCustomMappings
    {
        public string RecipientFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
