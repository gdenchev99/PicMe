namespace SocialMedia.Web.ViewModels.Posts
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using System;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");

        public string MediaExtension { get; set; }

        public string MediaPublicId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            // Get the file extension instead of the entire url.
            configuration.CreateMap<Post, PostViewModel>().ForMember(
                m => m.MediaExtension,
                opt => opt.MapFrom(x => x.MediaSource.Substring(x.MediaSource.LastIndexOf('.'))));
        }
    }
}
