namespace SocialMedia.Web.ViewModels.Posts
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class ProfilePostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string MediaSource { get; set; }
    }
}
