using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task DeleteAsync(RefreshToken token);
        Task<bool> SaveChangesAsync();
    }
}
