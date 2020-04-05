namespace SocialMedia.Web.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel
    {
        public string UserOneId { get; set; }

        public string UserTwoUsername { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
