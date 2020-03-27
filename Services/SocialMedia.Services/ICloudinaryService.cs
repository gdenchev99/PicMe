namespace SocialMedia.Services
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<RawUploadResult> UploadFileAsync(IFormFile file, string userId);

        Task DeleteFileAsync(string publicId);
    }
}
