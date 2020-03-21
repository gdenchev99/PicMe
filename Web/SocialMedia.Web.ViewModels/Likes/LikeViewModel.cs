namespace SocialMedia.Web.ViewModels.Likes
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class LikeViewModel : IMapFrom<Like>
    {
        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }
    }
}
