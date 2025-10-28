using BlogPlatform.Context;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user) =>
            await _context.Users.AddAsync(user);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
