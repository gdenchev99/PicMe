namespace SocialMedia.Web.ViewModels.Notifications
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Info { get; set; }

        public string PostMediaSource { get; set; }

        public string PostMediaPublicId { get; set; }
    }
}
