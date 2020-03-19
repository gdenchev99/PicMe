namespace SocialMedia.Data.Models
{
    using SocialMedia.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
