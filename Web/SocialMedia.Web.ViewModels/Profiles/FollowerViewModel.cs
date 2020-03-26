namespace SocialMedia.Web.ViewModels.Profiles
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class FollowerViewModel : IMapFrom<UserFollower>
    {
        public string FollowerFirstName { get; set; }

        public string FollowerUserName { get; set; }

        public string FollowerProfilePictureUrl { get; set; }
    }
}
