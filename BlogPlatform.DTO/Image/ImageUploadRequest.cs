using Microsoft.AspNetCore.Http;

namespace BlogPlatform.DTO.Image
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; } = null!;
    }
}
