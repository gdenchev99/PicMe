namespace SocialMedia.Web.ViewModels.Profiles
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserPostModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string MediaSource { get; set; }
    }
}
