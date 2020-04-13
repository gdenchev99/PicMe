namespace SocialMedia.Web.ViewModels.Profiles
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserFollowerModel : IMapFrom<UserFollower>
    {
        public string FollowerUserName { get; set; }

        public bool IsApproved { get; set; }
    }
}
