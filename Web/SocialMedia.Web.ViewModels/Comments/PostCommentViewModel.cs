namespace SocialMedia.Web.ViewModels.Comments
{
    using System;
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        // Random key for mapping(foreach in react) - to be fixed.
        public int CustomId => new Random().Next() + new Random().Next();

        public string CreatorUserName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");
    }
}
