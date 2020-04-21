namespace SocialMedia.Services.Data.Tests
{
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class EncodeServiceTests
    {
        [Fact]
        public void Base64Decode_ShouldDecodeString()
        {
            // Arrange
            string encodedText = "cmFuZG9tIHRleHQ=";
            var service = new EncodeService();

            // Act
            var expectedResult = "random text"; // The decoded version of the encodedText.
            var actualResult = service.Base64Decode(encodedText);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Base64Encode_ShouldEncodeString()
        {
            // Arrange
            string text = "random text";
            var service = new EncodeService();

            // Act
            var expectedResult = "cmFuZG9tIHRleHQ="; // string text - encoded using an online encoder.
            var actualResult = service.Base64Encode(text);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
