using BlogPlatform.DTO.Tag;
using BlogPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            return tag == null ? NotFound() : Ok(tag);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TagRequest request)
        {
            var success = await _tagService.CreateAsync(request);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TagRequest request)
        {
            var success = await _tagService.UpdateAsync(id, request);
            return success ? Ok() : NotFound();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _tagService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
