namespace SocialMedia.Web.ViewModels.Profiles
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class UploadPictureInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
