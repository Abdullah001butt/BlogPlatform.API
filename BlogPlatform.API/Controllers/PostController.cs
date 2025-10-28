using BlogPlatform.DTO.Post;
using BlogPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var posts = await _postService.SearchAsync(keyword);
            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await _postService.CreateAsync(request, userId);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePostRequest request)
        {
            var success = await _postService.UpdateAsync(id, request);
            return success ? Ok() : NotFound();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _postService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
