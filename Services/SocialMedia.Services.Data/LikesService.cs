namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Likes;

    public class LikesService : ILikesService
    {
        private readonly IRepository<Like> repository;

        public LikesService(IRepository<Like> repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(AddLikeModel model)
        {
            var like = new Like
            {
                UserId = model.UserId,
                PostId = model.PostId,
            };

            var likeExists = this.repository.All()
                .Where(l => l.PostId == model.PostId)
                .Any(l => l.UserId == model.UserId);

            if (likeExists)
            {
                return "You have already liked this post";
            }

            await this.repository.AddAsync(like);

            var result = await this.repository.SaveChangesAsync() > 0;

            if (!result)
            {
                return "Something went wrong.";
            }

            return "Post liked successfully.";
        }

        public async Task<IEnumerable<LikeViewModel>> GetLastThreeAsync(int postId)
        {
            var likes = await this.repository.All()
                .Where(l => l.PostId == postId)
                .OrderByDescending(l => l.CreatedOn)
                .Take(3)
                .To<LikeViewModel>()
                .ToListAsync();

            if (likes.Count < 0)
            {
                return null;
            }

            return likes;
        }

        public bool IsPostLikedByUser(string userId, int postId)
        {
            var like = this.repository.All()
                .Any(l => l.PostId == postId && l.UserId == userId);

            if (!like)
            {
                return false;
            }

            return true;
        }

        public async Task<string> RemoveAsync(AddLikeModel model)
        {
            var like = this.repository.All()
                .FirstOrDefault(l => l.PostId == model.PostId && l.UserId == model.UserId);

            if (like == null)
            {
                return "You haven't liked this post";
            }

            this.repository.Delete(like);

            var result = await this.repository.SaveChangesAsync() > 0;

            if (!result)
            {
                return "Like could not be removed";
            }

            return "Like removed successfully";
        }
    }
}
