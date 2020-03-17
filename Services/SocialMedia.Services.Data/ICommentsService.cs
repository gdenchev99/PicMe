namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<bool> CreateAsync(CommentCreateModel model);

        Task<IEnumerable<CommentViewModel>> GetPostCommentsAsync(int postId);
    }
}
