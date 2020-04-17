namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<string> CreateAsync(CommentCreateModel model);

        Task DeleteAsync(int id);

        Task<IEnumerable<PostCommentViewModel>> GetPostCommentsAsync(int postId, int skipCount, int takeCount);

        Task<IEnumerable<FeedCommentViewModel>> GetLastTwoAsync(int postId);

        Task<bool> ExistsAsync(int id);
    }
}
