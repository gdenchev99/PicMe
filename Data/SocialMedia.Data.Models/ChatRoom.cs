namespace SocialMedia.Data.Models
{
    using System.Collections.Generic;

    public class ChatRoom
    {
        public ChatRoom()
        {
            this.UserChatRooms = new HashSet<UserChatRoom>();
            this.Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public ICollection<UserChatRoom> UserChatRooms { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
