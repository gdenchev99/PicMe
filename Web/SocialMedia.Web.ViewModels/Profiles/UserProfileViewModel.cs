namespace SocialMedia.Web.ViewModels.Profiles
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool IsPrivate { get; set; }

        public string Bio { get; set; }

        public ICollection<UserFollowerModel> Followers { get; set; }

        public ICollection<UserFollowingModel> Followings { get; set; }
    }
}
