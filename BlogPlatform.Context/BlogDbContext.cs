using BlogPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<PostTag> PostTags => Set<PostTag>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        }
    }
}
