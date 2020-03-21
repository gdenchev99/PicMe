namespace SocialMedia.Services.Data
{
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Profiles;

    public interface IProfilesService
    {
        Task<UserProfileViewModel> GetUserProfileAsync(string username);

        Task<string> AddFollowerAsync(AddFollowerModel model);
    }
}
