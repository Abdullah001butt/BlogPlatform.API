using BlogPlatform.DTO.Auth;

namespace BlogPlatform.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
