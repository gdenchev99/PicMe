using System.Threading.Tasks;

namespace SocialMedia.Features.Notifications
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(string info);
    }
}
