namespace BlogPlatform.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
