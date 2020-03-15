namespace SocialMedia.Services.Data
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Posts;
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

        public async Task<bool> CreateAsync(PostCreateModel postCreateModel)
        {
            var post = new Post
            {
                Description = postCreateModel.Description,
                MediaSource = postCreateModel.MediaSource,
                CreatorId = postCreateModel.CreatorId,
            };

            await this.postRepository.AddAsync(post);

            var result = await this.postRepository.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<IEnumerable<AllPostsViewModel>> GetAllAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            var posts = await this.postRepository.All()
                .Where(p => p.Creator.Followers.Any(x => x.FollowerId == id))
                .To<AllPostsViewModel>()
                .ToListAsync();

            if (posts == null)
            {
                return null;
            }

            return posts;
        }
    }
}
