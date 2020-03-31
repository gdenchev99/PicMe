namespace SocialMedia.Web.ViewModels.Comments
{
    using System;
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");
    }
}
