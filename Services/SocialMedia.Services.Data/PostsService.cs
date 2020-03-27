namespace SocialMedia.Services.Data
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Posts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ICloudinaryService cloudinaryService;

        public PostsService(IDeletableEntityRepository<Post> postRepository, ICloudinaryService cloudinaryService)
        {
            this.postRepository = postRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> CreateAsync(PostCreateModel postCreateModel)
        {
            var uploadResult = await this.cloudinaryService.UploadFileAsync(postCreateModel.MediaSource, postCreateModel.CreatorId);
            var mediaUrl = uploadResult.SecureUri.ToString();
            var publicId = uploadResult.PublicId;


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

            if (post == null)
            {
                return false;
            }

            var publicId = post.MediaPublicId;

            // Delete the media from Cloud
            await this.cloudinaryService.DeleteFileAsync(publicId);

            // Delete the entire post
            this.postRepository.HardDelete(post);

            var result = await this.postRepository.SaveChangesAsync() > 0;

            return result;
        }

        // id = id of currently logged in user.
        public async Task<IEnumerable<FeedViewModel>> GetAllAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            var posts = await this.postRepository.All()
                .Where(p => p.Creator.Followers.Any(x => x.FollowerId == id))
                .To<FeedViewModel>()
                .ToListAsync();

            if (posts == null)
            {
                return null;
            }

            return posts;
        }

        // id = id of post
        public async Task<PostViewModel> GetAsync(int id)
        {
            if (id < 0)
            {
                return null;
            }

            var post = await this.postRepository.All()
                .Where(p => p.Id == id)
                .To<PostViewModel>()
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return null;
            }

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
    }
}
