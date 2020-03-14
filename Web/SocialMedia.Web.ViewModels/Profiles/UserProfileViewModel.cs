namespace SocialMedia.Web.ViewModels.Profiles
{
    using System.Collections.Generic;

    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ICollection<UserPostModel> Posts { get; set; }

        public ICollection<UserFollowerModel> Followers { get; set; }

        public ICollection<UserFollowingModel> Followings { get; set; }

        public int PostsCount => this.Posts.Count;

        public int FollowersCount => this.Followers.Count;

        public int FollowingsCount => this.Followings.Count;
    }
}
