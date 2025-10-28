using BlogPlatform.DTO.Category;
using BlogPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var success = await _categoryService.CreateAsync(request);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryRequest request)
        {
            var success = await _categoryService.UpdateAsync(id, request);
            return success ? Ok() : NotFound();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _categoryService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
