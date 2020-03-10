using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Web.ViewModels.Posts
{
    public class PostInputModel
    {
        public string Description { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string MediaSource { get; set; }

        public string CreatorId { get; set; }
    }
}
