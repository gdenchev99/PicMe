namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Likes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task<string> AddAsync(AddLikeModel model);

        Task<string> RemoveAsync(AddLikeModel model);

        bool IsPostLikedByUser(string userId, int postId);

        Task<IEnumerable<LikeViewModel>> GetLastThreeAsync(int postId);

        Task<bool> ExistsAsync(int id);
    }
}
