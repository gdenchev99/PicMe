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
        private readonly IEncodeService encodeService;

        public CloudinaryService(IOptions<CloudinaryConfig> config, IEncodeService encodeService)
        {
            this.config = config;
            this.encodeService = encodeService;
        }

        /*
         The method uploads files to Cloudinary asynchronosly and returns UploadResult
         */
        public async Task<RawUploadResult> UploadFileAsync(IFormFile inputFile, string userId)
        {
            var cloudinary = this.Cloudinary();

            var fileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff");

            // Encode the uploader's id to Base64. Better encryption later on.
            var encodedUserId = this.encodeService.Base64Encode(userId);

            /* Set the folder to which the pictures will be uploaded as the ecnoded id + the current days date,
            so the images are sorted for each user. */
            var folder = encodedUserId + "/" + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");

            var file = new FileDescription(fileName, inputFile.OpenReadStream());

            var uploadParams = new RawUploadParams
            {
                File = file,
                Folder = folder,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }

        /*
         The method deletes a file from Cloudinary asynchronosly
         */
        public async Task DeleteFileAsync(string publicId)
        {
            var cloudinary = this.Cloudinary();

            var deletionParams = new DeletionParams(publicId);

            await cloudinary.DestroyAsync(deletionParams);
        }

        /*
         Create an instance of Cloudinary using the configuration from appsettings.
         */
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
