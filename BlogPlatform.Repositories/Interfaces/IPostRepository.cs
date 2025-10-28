using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> SearchAsync(string keyword);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
        Task<bool> SaveChangesAsync();
    }
}
