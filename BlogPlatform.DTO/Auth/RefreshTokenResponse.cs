namespace BlogPlatform.DTO.Auth
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
