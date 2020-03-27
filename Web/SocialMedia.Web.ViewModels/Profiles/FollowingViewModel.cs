namespace SocialMedia.Web.ViewModels.Profiles
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class FollowingViewModel : IMapFrom<UserFollower>
    {
        public string UserFirstName { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }
    }
}
