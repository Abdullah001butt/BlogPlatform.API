namespace BlogPlatform.DTO.Post
{
    public class UpdatePostRequest
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
        public string? ImageUrl { get; set; }
    }
}
