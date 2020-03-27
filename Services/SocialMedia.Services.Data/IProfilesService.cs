namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Profiles;

    public interface IProfilesService
    {
        Task<UserProfileViewModel> GetUserProfileAsync(string username);

        Task<string> AddFollowerAsync(AddFollowerModel model);

        Task<string> RemoveFollowerAsync(AddFollowerModel model);

        Task<IEnumerable<FollowerViewModel>> GetUserFollowersAsync(string username);

        Task<IEnumerable<FollowingViewModel>> GetUserFollowingsAsync(string username);

    }
}
