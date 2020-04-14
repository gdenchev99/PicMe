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

            if (!result)
            {
                throw new DbUpdateException("Could not add comment to the database.");
            }

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

            if (comment == null)
            {
                throw new ArgumentNullException("Comment could not be found");
            }

            this.commentRepository.HardDelete(comment);
            var result = await this.commentRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<FeedCommentViewModel>> GetLastTwoAsync(int postId)
        {
            if (postId <= 0)
            {
                throw new ArgumentException("The provided id is invalid");
            }

            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .Take(2)
                .OrderBy(c => c.CreatedOn)
                .To<FeedCommentViewModel>()
                .ToListAsync();

            if (comments.Count <= 0)
            {
                return null;
            }

            return comments;
        }

        public async Task<IEnumerable<PostCommentViewModel>> GetPostCommentsAsync(int postId, int skipCount, int takeCount)
        {
            if (postId <= 0)
            {
                throw new ArgumentException("The provided id is invalid.");
            }

            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .Skip(skipCount)
                .Take(takeCount)
                .To<PostCommentViewModel>()
                .ToListAsync();

            if (comments.Count <= 0)
            {
                return null;
            }

            return comments;
        }
    }
}
