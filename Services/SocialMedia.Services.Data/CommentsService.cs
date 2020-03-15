using SocialMedia.Data.Common.Repositories;
using SocialMedia.Data.Models;
using SocialMedia.Web.ViewModels.Comments;
using System.Threading.Tasks;

namespace SocialMedia.Services.Data
{
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
    }
}
