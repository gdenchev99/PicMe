using SocialMedia.Web.ViewModels.Posts;

namespace SocialMedia.Services
{
    public interface IPostsService
    {
        void CreateAsync(PostInputModel model);
    }
}
