using BlogPlatform.DTO.Image;
using BlogPlatform.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace BlogPlatform.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(ImageUploadRequest request)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(request.File.FileName, request.File.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Image upload failed: " + result.Error?.Message);

            return result.SecureUrl.ToString();
        }
    }
}
