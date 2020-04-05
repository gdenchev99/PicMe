namespace SocialMedia.Features.Chat
{
    using SocialMedia.Web.ViewModels.Messages;
    using System.Threading.Tasks;

    public interface IChatClient
    {
        Task ReceiveMessage(MessageViewModel message);
    }
}
