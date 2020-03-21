namespace SocialMedia.Services.Data
{
    using SocialMedia.Web.ViewModels.Likes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task<string> AddAsync(LikeInputModel model);

        Task<string> RemoveAsync(LikeInputModel model);

        bool IsPostLikedByUser(string userId, int postId);

        Task<IEnumerable<LikeViewModel>> GetLastThreeAsync(int postId);
    }
}
