namespace SocialMedia.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SocialMedia.Helpers;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class BlobService : IBlobService
    {
        private readonly IOptions<BlobConfiguration> blobConfiguration;
        private readonly string pathPrefix;

        public BlobService(IOptions<BlobConfiguration> blobConfiguration)
        {
            this.blobConfiguration = blobConfiguration;
            this.pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
        }

        public async Task DeleteImageAsync(string fileUrl)
        {
            var uri = new Uri(fileUrl);
            var blobName = Path.GetFileName(uri.LocalPath);

            var container = this.GetCloudBlobContainer("images");
            var blobDirectory = container.GetDirectoryReference(this.pathPrefix);
            var blockBlob = blobDirectory.GetBlobReference(blobName);

            await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            try
            {
                // Get the base Uri from the appsettings file in SocialMedia.App
                var baseUri = new Uri(this.blobConfiguration.Value.BaseUri);

                var streamReader = new StreamReader(image.OpenReadStream());
                var container = this.GetCloudBlobContainer("images");
                var fileName = this.GenerateFileName(Path.GetFileNameWithoutExtension(image.FileName));
                var blob = container.GetBlockBlobReference(fileName + ".jpg");

                await blob.UploadFromStreamAsync(streamReader.BaseStream);

                return new Uri(baseUri, $"/images/{fileName}.jpg").ToString();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = this.pathPrefix +
                DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff");
            return strFileName;
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(this.blobConfiguration.Value.ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);

            return container;
        }
    }
}
