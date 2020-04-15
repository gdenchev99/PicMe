namespace SocialMedia.Web.ViewModels.Posts
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class ProfilePostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string MediaExtension { get; set; }

        public string MediaPublicId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            // Get the file extension instead of the entire url.
            configuration.CreateMap<Post, ProfilePostViewModel>().ForMember(
                m => m.MediaExtension,
                opt => opt.MapFrom(x => x.MediaSource.Substring(x.MediaSource.LastIndexOf('.'))));
        }
    }
}
