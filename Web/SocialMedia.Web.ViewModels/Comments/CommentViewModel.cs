namespace SocialMedia.Web.ViewModels.Comments
{
    using System;

    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string CreatorUserName { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormat => this.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM");
    }
}
