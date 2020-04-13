namespace SocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SocialMedia.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Notifications = new HashSet<Notification>();
        }

        public string Description { get; set; }

        [Required]
        public string MediaSource { get; set; }

        public string MediaPublicId { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
