namespace SocialMedia.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SocialMedia.Data.Common.Models;

    public class Message : BaseModel<int>
    {
        public string FromUserId { get; set; }

        public ApplicationUser FromUser { get; set; }

        public string ToUserId { get; set; }

        public ApplicationUser ToUser { get; set; }

        [Required]
        [MaxLength(800)]
        public string Text { get; set; }
    }
}
