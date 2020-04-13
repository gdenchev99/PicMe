namespace SocialMedia.Data.Models
{
    using System.Collections.Generic;

    public class ChatRoom
    {
        public ChatRoom()
        {
            this.Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public string UserOneId { get; set; }

        public virtual ApplicationUser UserOne { get; set; }

        public string UserTwoId { get; set; }

        public virtual ApplicationUser UserTwo { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
