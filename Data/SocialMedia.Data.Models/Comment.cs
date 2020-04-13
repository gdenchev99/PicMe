namespace SocialMedia.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SocialMedia.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {

        }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
