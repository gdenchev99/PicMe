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
        private readonly Uri baseUri = new Uri(@"https://smapp.blob.core.windows.net");

        public BlobService(IOptions<BlobConfiguration> blobConfiguration)
        {
            this.blobConfiguration = blobConfiguration;
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            try
            {
                var streamReader = new StreamReader(image.OpenReadStream());
                var container = this.GetCloudBlobContainer("images");
                var blob = container.GetBlockBlobReference(image.FileName + ".jpg");
                await blob.UploadFromStreamAsync(streamReader.BaseStream);

                return new Uri(this.baseUri, $"/images/{image.FileName}.jpg").ToString();
            }
            catch (Exception exception)
            {
                throw;
            }
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
