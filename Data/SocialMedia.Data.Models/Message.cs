﻿namespace SocialMedia.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SocialMedia.Data.Common.Models;

    public class Message : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; }

        [Required]
        [MaxLength(800)]
        public string Text { get; set; }
    }
}
