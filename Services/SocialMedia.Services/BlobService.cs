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
                var fileName = this.GenerateFileName(Path.GetFileNameWithoutExtension(image.FileName));
                var blob = container.GetBlockBlobReference(fileName + ".jpg");
                await blob.UploadFromStreamAsync(streamReader.BaseStream);

                return new Uri(this.baseUri, $"/images/{fileName}.jpg").ToString();
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
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" +
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
