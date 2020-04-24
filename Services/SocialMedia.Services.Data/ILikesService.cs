namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Likes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task<int> GetPostLikesAsync(int postId);

        Task<string> AddAsync(AddLikeModel model);

        Task<bool> RemoveAsync(AddLikeModel model);

        Task<bool> IsPostLikedByUserAsync(string userId, int postId);

        Task<IEnumerable<LikeViewModel>> GetLastThreeAsync(int postId);

        Task<bool> ExistsAsync(int id);
    }
}
