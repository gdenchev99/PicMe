namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<bool> CreateAsync(CommentCreateModel model);

        Task<IEnumerable<PostCommentViewModel>> GetPostCommentsAsync(int postId);

        Task<IEnumerable<FeedCommentViewModel>> GetLastTwoAsync(int postId);

    }
}
