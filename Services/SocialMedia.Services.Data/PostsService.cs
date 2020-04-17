namespace SocialMedia.Services.Data
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Posts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;

        public PostsService(IDeletableEntityRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<bool> CreateAsync(PostCreateModel postCreateModel, string mediaUrl, string publicId)
        {
            var post = new Post
            {
                Description = postCreateModel.Description,
                MediaSource = mediaUrl,
                MediaPublicId = publicId,
                CreatorId = postCreateModel.CreatorId,
            };

            await this.postRepository.AddAsync(post);

            var result = await this.postRepository.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = this.postRepository.All()
                .FirstOrDefault(p => p.Id == id);

            // Delete the entire post
            this.postRepository.HardDelete(post);

            var result = await this.postRepository.SaveChangesAsync() > 0;

            return result;
        }

        // id = id of currently logged in user.
        public async Task<IEnumerable<FeedViewModel>> GetFeedAsync(string id, int skipCount, int takeCount)
        {
            var posts = await this.postRepository.All()
                .Where(p => p.Creator.Followers.Any(x => x.FollowerId == id && x.IsApproved == true))
                .OrderByDescending(p => p.CreatedOn)
                .Skip(skipCount)
                .Take(takeCount)
                .To<FeedViewModel>()
                .ToListAsync();

            return posts;
        }

        // id = id of post
        public async Task<PostViewModel> GetAsync(int id)
        {
            var post = await this.postRepository.All()
                .Where(p => p.Id == id)
                .To<PostViewModel>()
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<IEnumerable<ProfilePostViewModel>> GetProfilePostsAsync(string username)
        {
            var posts = await this.postRepository.All()
                .Where(p => p.Creator.UserName == username)
                .OrderByDescending(p => p.CreatedOn)
                .To<ProfilePostViewModel>()
                .ToListAsync();

            return posts;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var post = await this.postRepository.All()
                .FirstOrDefaultAsync(p => p.Id == id);

            bool exists = post != null;

            return exists;
        }
    }
}
