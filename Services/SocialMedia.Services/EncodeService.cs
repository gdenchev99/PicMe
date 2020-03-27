namespace SocialMedia.Services
{
    using System;
    using System.Text;

    public class EncodeService : IEncodeService
    {
        public string Base64Decode(string base64Data)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64Data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Base64Encode(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
    }
}
