namespace SocialMedia.Services.Data
{
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

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<bool> CreateAsync(CommentCreateModel model)
        {
            var comment = new Comment
            {
                Text = model.Text,
                CreatorId = model.CreatorId,
                PostId = model.PostId,
            };

            await this.commentRepository.AddAsync(comment);

            var result = await this.commentRepository.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<IEnumerable<FeedCommentViewModel>> GetLastTwoAsync(int postId)
        {
            if (postId <= 0)
            {
                return null;
            }

            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .Take(2)
                .To<FeedCommentViewModel>()
                .ToListAsync();

            if (comments.Count <= 0)
            {
                return null;
            }

            return comments;
        }

        public async Task<IEnumerable<PostCommentViewModel>> GetPostCommentsAsync(int postId)
        {
            if (postId <= 0)
            {
                return null;
            }

            var comments = await this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
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
