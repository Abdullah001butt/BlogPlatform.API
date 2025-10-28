namespace BlogPlatform.DTO.Post
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorUsername { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public List<string> Tags { get; set; } = new();
    }
}
