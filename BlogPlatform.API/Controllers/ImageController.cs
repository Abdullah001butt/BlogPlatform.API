using BlogPlatform.DTO.Image;
using BlogPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequest request)
        {
            var imageUrl = await _imageService.UploadImageAsync(request);
            return Ok(new { Url = imageUrl });
        }
    }
}
