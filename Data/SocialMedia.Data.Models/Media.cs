using SocialMedia.Data.Common.Models;
using SocialMedia.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Data.Models
{
    public class Media : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        [Required]
        public MediaType Type { get; set; }

        [Required]
        public string Source { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
