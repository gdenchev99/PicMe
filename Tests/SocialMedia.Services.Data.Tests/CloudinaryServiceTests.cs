namespace SocialMedia.Services.Data.Tests
{
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Moq;
    using SocialMedia.App;
    using SocialMedia.Helpers;
    using Xunit;

    public class CloudinaryServiceTests
    {
        [Fact]
        public async Task UploadFileAsync_WithValidFile_ShouldUploadFile()
        {
            // Arrange
            var directory = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.Development.json", false)
                .Build();

            var config = Options.Create(configuration.GetSection("Cloudinary").Get<CloudinaryConfig>());
            var encodeService = new EncodeService();
            var service = new CloudinaryService(config, encodeService);

            var fileMock = this.SetupMockFile();
            var file = fileMock.Object;

            // Act
            var result = await service.UploadFileAsync(file, "random-string-for-id");
            bool urlExists = result.SecureUri.ToString().Length > 0;

            // Assert
            Assert.True(urlExists);
        }

        private Mock<IFormFile> SetupMockFile()
        {
            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock;
        }
    }
}
