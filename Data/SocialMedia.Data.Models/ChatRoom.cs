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

        public ApplicationUser UserOne { get; set; }

        public string UserTwoId { get; set; }

        public ApplicationUser UserTwo { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
