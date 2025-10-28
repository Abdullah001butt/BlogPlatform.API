using BlogPlatform.Context;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly BlogDbContext _context;

        public RefreshTokenRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token) =>
            await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

        public async Task AddAsync(RefreshToken token) =>
            await _context.RefreshTokens.AddAsync(token);

        public async Task DeleteAsync(RefreshToken token) =>
            _context.RefreshTokens.Remove(token);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
