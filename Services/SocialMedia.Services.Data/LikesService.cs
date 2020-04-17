namespace SocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Likes;

    public class LikesService : ILikesService
    {
        private readonly IRepository<Like> likeRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Post> postRepository;

        public LikesService(
            IRepository<Like> likeRepository,
            IRepository<ApplicationUser> userRepository,
            IRepository<Post> postRepository)
        {
            this.likeRepository = likeRepository;
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        public async Task<string> AddAsync(AddLikeModel model)
        {
            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.Id == model.UserId);

            var post = await this.postRepository.All()
                .FirstOrDefaultAsync(p => p.Id == model.PostId);

            var like = new Like
            {
                User = user,
                Post = post,
            };

            var likeExists = this.likeRepository.All()
                .Where(l => l.PostId == model.PostId)
                .Any(l => l.UserId == model.UserId);

            await this.likeRepository.AddAsync(like);

            var result = await this.likeRepository.SaveChangesAsync() > 0;

            var postCreatorId = await this.likeRepository.All()
                .Where(l => l.PostId == model.PostId)
                .Select(l => l.Post.CreatorId)
                .FirstOrDefaultAsync();

            return postCreatorId;
        }

        public async Task<IEnumerable<LikeViewModel>> GetLastThreeAsync(int postId)
        {
            var likes = await this.likeRepository.All()
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
            var like = this.likeRepository.All()
                .Any(l => l.PostId == postId && l.UserId == userId);

            if (!like)
            {
                return false;
            }

            return true;
        }

        public async Task<string> RemoveAsync(AddLikeModel model)
        {
            var like = this.likeRepository.All()
                .FirstOrDefault(l => l.PostId == model.PostId && l.UserId == model.UserId);

            this.likeRepository.Delete(like);

            var result = await this.likeRepository.SaveChangesAsync() > 0;

            return "Like removed successfully";
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var like = await this.likeRepository.All()
                .FirstOrDefaultAsync(l => l.Id == id);

            bool exists = like != null;

            return exists;
        }
    }
}
