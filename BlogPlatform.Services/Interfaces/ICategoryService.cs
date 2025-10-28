using BlogPlatform.DTO.Category;

namespace BlogPlatform.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CategoryRequest request);
        Task<bool> UpdateAsync(Guid id, CategoryRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
