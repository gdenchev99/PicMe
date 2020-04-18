namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Notifications;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        Task CreateNotificationAsync(string userId, int? postId, string info);

        Task<IEnumerable<NotificationViewModel>> GetNotificationsAsync(string userId, int skipCount, int takeCount);

        Task<int> GetUnreadNotificationsCountAsync(string userId);

        Task<string> UpdateReadStatusAsync(string userId);
    }
}
