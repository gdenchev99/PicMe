namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Profiles;

    public interface IProfilesService
    {
        Task<UserProfileViewModel> GetUserProfileAsync(string username);

        Task<bool> AddFollowerAsync(AddFollowerModel model);

        Task<string> RemoveFollowerAsync(AddFollowerModel model);

        Task<IEnumerable<FollowerViewModel>> GetUserFollowersAsync(string username);

        Task<IEnumerable<FollowingViewModel>> GetUserFollowingsAsync(string username);

        Task<IEnumerable<RequestViewModel>> GetFollowerRequestsAsync(string id);

        Task<bool> ApproveRequestAsync(string username);

        Task<bool> DeleteRequestAsync(string username);

        Task<string> UploadProfilePictureAsync(string id, string profilePictureUrl, string picturePublicId);

        Task<IEnumerable<ProfileSearchViewModel>> SearchProfilesAsync(string searchString);

        Task<bool> UserExistsByIdAsync(string userId);

        Task<bool> UserExistsByNameAsync(string username);

        Task<bool> FollowerExistsAsync(string userId, string followerId);
    }
}
