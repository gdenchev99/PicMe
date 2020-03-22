namespace SocialMedia.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IBlobService
    {
        Task<string> UploadImageAsync(IFormFile image);

        Task DeleteImageAsync(string fileUrl);
    }
}
