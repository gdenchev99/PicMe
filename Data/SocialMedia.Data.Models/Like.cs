namespace SocialMedia.Data.Models
{
    using SocialMedia.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        public Like()
        {

        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
