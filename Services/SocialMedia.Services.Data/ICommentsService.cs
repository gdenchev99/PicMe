namespace SocialMedia.Services.Data
{
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<bool> CreateAsync(CommentCreateModel model);
    }
}
