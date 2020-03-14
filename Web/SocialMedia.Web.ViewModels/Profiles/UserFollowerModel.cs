namespace SocialMedia.Web.ViewModels.Profiles
{
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserFollowerModel : IMapFrom<UserFollower>
    {
        public string FollowerUserName { get; set; }
    }
}
