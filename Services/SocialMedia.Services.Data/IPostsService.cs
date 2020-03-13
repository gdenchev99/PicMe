namespace SocialMedia.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SocialMedia.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<bool> CreateAsync(PostCreateModel postCreateModel);

        Task<IEnumerable<AllPostsViewModel>> GetAllAsync(string id);
    }
}
