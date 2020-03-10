namespace SocialMedia.Data.Models
{
    using System;

    using SocialMedia.Data.Common.Models;

    public class UserFollower : IAuditInfo
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string FollowerId { get; set; }

        public ApplicationUser Follower { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
