using BlogPlatform.Domain.Entities;
using BlogPlatform.DTO.Category;
using BlogPlatform.Repositories.Interfaces;
using BlogPlatform.Services.Interfaces;

namespace BlogPlatform.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> CreateAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            };

            await _categoryRepo.AddAsync(category);
            return await _categoryRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepo.DeleteAsync(category);
            return await _categoryRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            });
        }

        public async Task<CategoryResponse?> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryRequest request)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return false;

            category.Name = request.Name;
            category.Description = request.Description;

            await _categoryRepo.UpdateAsync(category);
            return await _categoryRepo.SaveChangesAsync();
        }
    }
}
