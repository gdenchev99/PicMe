namespace SocialMedia.Data.Models
{
    public class UserChatRoom
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; }
    }
}
