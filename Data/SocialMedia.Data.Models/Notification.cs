namespace SocialMedia.Data.Models
{
    using SocialMedia.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Notification : BaseModel<int>
    {
        public Notification()
        {

        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string Info { get; set; }

        public bool Read { get; set; }
    }
}
