using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Web.ViewModels.Comments
{
    public class CommentCreateModel
    {
        [MaxLength(300, ErrorMessage = "Your message cannot be longer than 300 symbols.")]
        [Required]
        public string Text { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
