namespace SocialMedia.Data.Models
{
    using System;

    using SocialMedia.Data.Common.Models;

    public class UserFollower : IAuditInfo
    {
        public UserFollower()
        {

        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string FollowerId { get; set; }

        public virtual ApplicationUser Follower { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
