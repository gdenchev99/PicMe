namespace SocialMedia.Web.ViewModels.Likes
{
    using System.ComponentModel.DataAnnotations;

    public class AddLikeModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
