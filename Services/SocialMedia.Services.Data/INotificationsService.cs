namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Notifications;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        Task CreateNotificationAsync(string userId, int postId, string info);

        Task<IEnumerable<NotificationViewModel>> GetNotificationsAsync(string userId);

        Task<int> GetUnreadNotificationsAsync(string userId);

        Task<string> UpdateReadStatusAsync(string userId);
    }
}
