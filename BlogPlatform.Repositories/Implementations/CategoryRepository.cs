using BlogPlatform.Context;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _context;

        public CategoryRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category) =>
            await _context.Categories.AddAsync(category);

        public async Task DeleteAsync(Category category) =>
            _context.Categories.Remove(category);

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await _context.Categories.FindAsync(id);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public async Task UpdateAsync(Category category) =>
             _context.Categories.Update(category);
    }
}
