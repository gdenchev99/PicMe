namespace SocialMedia.Web.ViewModels.Posts
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using System;

    public class PostViewModel : IMapFrom<Post>
    {
        public string Description { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");

        public string MediaSource { get; set; }

        public string MediaPublicId { get; set; }
    }
}
