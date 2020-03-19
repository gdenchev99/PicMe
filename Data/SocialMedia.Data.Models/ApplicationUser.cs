// ReSharper disable VirtualMemberCallInConstructor
namespace SocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SocialMedia.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Followers = new HashSet<UserFollower>();
            this.Followings = new HashSet<UserFollower>();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
        }

        // Personal Data
        [Required]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string ProfilePictureUrl { get; set; }

        // Followers
        public ICollection<UserFollower> Followers { get; set; }

        public ICollection<UserFollower> Followings { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
