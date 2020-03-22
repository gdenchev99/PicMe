namespace SocialMedia.Web.ViewModels.Comments
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class FeedCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string CreatorUserName { get; set; }

        public string Text { get; set; }
    }
}
