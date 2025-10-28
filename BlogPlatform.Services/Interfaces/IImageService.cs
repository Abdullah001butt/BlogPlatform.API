using BlogPlatform.DTO.Image;

namespace BlogPlatform.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(ImageUploadRequest request);
    }
}
