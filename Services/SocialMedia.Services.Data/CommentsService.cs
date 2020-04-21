namespace SocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IRepository<Post> postRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository, IRepository<Post> postRepository)
        {
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
        }

        public async Task<string> CreateAsync(CommentCreateModel model)
        {
            var comment = new Comment
            {
                Text = model.Text,
                CreatorId = model.CreatorId,
                PostId = model.PostId,
            };

            await this.commentRepository.AddAsync(comment);

            var result = await this.commentRepository.SaveChangesAsync() > 0;

            var postCreatorId = await this.postRepository.All()
                .Where(p => p.Id == model.PostId)
                .Select(p => p.CreatorId)
                .FirstOrDefaultAsync();

            return postCreatorId;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await this.commentRepository.All()
                .FirstOrDefaultAsync(c => c.Id == id);

            this.commentRepository.HardDelete(comment);
            var result = await this.commentRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<FeedCommentViewModel>> GetLastTwoAsync(int postId)
        {
            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .Take(2)
                .OrderBy(c => c.CreatedOn)
                .To<FeedCommentViewModel>()
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<PostCommentViewModel>> GetPostCommentsAsync(int postId, int skipCount, int takeCount)
        {
            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .Skip(skipCount)
                .Take(takeCount)
                .To<PostCommentViewModel>()
                .ToListAsync();

            return comments;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var comment = await this.commentRepository.All()
                .FirstOrDefaultAsync(e => e.Id == id);

            bool exists = comment != null;

            return exists;
        }
    }
}
