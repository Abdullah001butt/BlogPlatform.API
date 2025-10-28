using BlogPlatform.Context;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _context;

        public TagRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetByIdAsync(Guid id) =>
            await _context.Tags.FindAsync(id);

        public async Task<IEnumerable<Tag>> GetAllAsync() =>
            await _context.Tags.ToListAsync();

        public async Task AddAsync(Tag tag) =>
            await _context.Tags.AddAsync(tag);

        public async Task UpdateAsync(Tag tag) =>
            _context.Tags.Update(tag);

        public async Task DeleteAsync(Tag tag) =>
            _context.Tags.Remove(tag);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
