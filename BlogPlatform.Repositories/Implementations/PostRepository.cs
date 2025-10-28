using BlogPlatform.Context;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post) =>
            await _context.Posts.AddAsync(post);

        public async Task DeleteAsync(Post post) =>
            _context.Posts.Remove(post);

        public async Task<IEnumerable<Post>> GetAllAsync() =>
            await _context.Posts.Include(p => p.Author).Include(p => p.Category).ToListAsync();

        public async Task<Post?> GetByIdAsync(Guid id) =>
            await _context.Posts.Include(p => p.Author).Include(p => p.Category).Include(p => p.PostTags).ThenInclude(pt => pt.Tag).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public async Task<IEnumerable<Post>> SearchAsync(string keyword) =>
            await _context.Posts.Where(p => p.Title.Contains(keyword) || p.Content.Contains(keyword)).ToListAsync();

        public async Task UpdateAsync(Post post) =>
             _context.Posts.Update(post);
    }
}
