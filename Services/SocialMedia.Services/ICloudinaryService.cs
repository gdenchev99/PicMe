namespace SocialMedia.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        public Task<string> UploadFileAsync(IFormFile file);
    }
}
