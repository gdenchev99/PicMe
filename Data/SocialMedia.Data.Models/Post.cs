﻿using SocialMedia.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Data.Models
{
    public class Post : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        [Required]
        public string MediaSource { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
