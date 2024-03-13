namespace Banlab.Social.Api.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImage(Stream stream, bool IsOriginalImage, string filename);
        Task<Stream> ResizeImage(Stream originalImage, int width, int height, bool resetStream);
    }
}
