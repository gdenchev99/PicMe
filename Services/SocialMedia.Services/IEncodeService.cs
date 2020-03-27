namespace SocialMedia.Services
{
    public interface IEncodeService
    {
        string Base64Encode(string text);

        string Base64Decode(string base64Data);
    }
}
