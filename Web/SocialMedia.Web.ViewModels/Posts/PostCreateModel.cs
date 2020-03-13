namespace SocialMedia.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class PostCreateModel
    {
        [MaxLength(300, ErrorMessage = "The maximum length of the description is 300 symbols.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Media file is required.")]
        public string MediaSource { get; set; }

        public string CreatorId { get; set; }
    }
}
