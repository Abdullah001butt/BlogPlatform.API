using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag tag);
        Task<bool> SaveChangesAsync();
    }
}
