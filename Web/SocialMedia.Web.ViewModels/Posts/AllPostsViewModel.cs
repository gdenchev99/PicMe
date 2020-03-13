namespace SocialMedia.Web.ViewModels.Posts
{
    using System;
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class AllPostsViewModel : IMapFrom<Post>
    {
        public string CreatorUserName { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");

        public string MediaSource { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }
    }
}
