namespace SocialMedia.Services.Data
{
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        public Task CreateAsync(MessageInputModel model);
    }
}
