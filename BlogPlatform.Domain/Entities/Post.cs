namespace BlogPlatform.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
