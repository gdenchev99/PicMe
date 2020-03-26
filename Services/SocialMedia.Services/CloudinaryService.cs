namespace SocialMedia.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using SocialMedia.Helpers;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly IOptions<CloudinaryConfig> config;

        public CloudinaryService(IOptions<CloudinaryConfig> config)
        {
            this.config = config;
        }

        public async Task<string> UploadFileAsync(IFormFile image)
        {
            var cloudinary = this.Cloudinary();

            var streamReader = new StreamReader(image.OpenReadStream());

            var fileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff");
            var folder = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");

            var file = new FileDescription(fileName, streamReader.BaseStream);

            var uploadParams = new RawUploadParams
            {
                File = file,
                Folder = folder,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUri.ToString();
        }

        private Cloudinary Cloudinary()
        {
            Account account = new Account(
                this.config.Value.CloudName,
                this.config.Value.ApiKey,
                this.config.Value.ApiSecret);

            Cloudinary cloudinary = new Cloudinary(account);

            return cloudinary;
        }
    }
}
