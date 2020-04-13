using System.Threading.Tasks;

namespace SocialMedia.Features.Chat
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(string info);
    }
}
