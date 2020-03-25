namespace SocialMedia.Features.Chat
{
    using System.Threading.Tasks;

    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
