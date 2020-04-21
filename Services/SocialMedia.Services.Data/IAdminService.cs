namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Administration;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<UsersViewModel>> GetUsersAsync();

        Task<string> BanUserAsync(string userId);

        Task UnbanUserAsync(string userId);

        Task<bool> IsAdminAsync(string id);
    }
}
