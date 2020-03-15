namespace SocialMedia.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SocialMedia.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
