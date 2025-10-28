namespace BlogPlatform.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Reader"!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
