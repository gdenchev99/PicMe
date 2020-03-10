using SocialMedia.Data.Common.Repositories;
using SocialMedia.Data.Models;
using SocialMedia.Web.ViewModels.Posts;

namespace SocialMedia.Services
{
    public class PostsService : IPostsService
    {
        private readonly IRepository<Post> repository;

        public PostsService(IRepository<Post> repository)
        {
            this.repository = repository;
        }

        public async CreateAsync(PostInputModel model)
        {
            this.repository.AddAsync
        }
    }
}
