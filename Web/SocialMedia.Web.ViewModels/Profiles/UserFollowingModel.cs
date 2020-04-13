namespace SocialMedia.Web.ViewModels.Profiles
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserFollowingModel : IMapFrom<UserFollower>
    {
        public string UserUserName { get; set; }

        public bool IsApproved { get; set; }
    }
}
