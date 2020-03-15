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
        }

        public string Description { get; set; }

        [Required]
        public string MediaSource { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
