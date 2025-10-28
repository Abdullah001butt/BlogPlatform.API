using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> SaveChangesAsync();
    }
}
