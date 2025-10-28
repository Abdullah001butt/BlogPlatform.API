using BlogPlatform.Domain.Entities;
using BlogPlatform.DTO.Auth;
using BlogPlatform.Repositories.Interfaces;
using BlogPlatform.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPlatform.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _tokenRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IRefreshTokenRepository tokenRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            _config = config;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await GenerateTokensAsync(user);
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var token = await _tokenRepo.GetByTokenAsync(request.RefreshToken);
            if (token == null || token.ExpiresAt < DateTime.UtcNow || token.IsRevoked)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await _userRepo.GetByIdAsync(token.UserId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            token.IsRevoked = true;
            await _tokenRepo.DeleteAsync(token);
            await _tokenRepo.SaveChangesAsync();

            var newToken = await GenerateTokensAsync(user);
            return new RefreshTokenResponse
            {
                AccessToken = newToken.AccessToken,
                ExpiresAt = newToken.ExpiresAt
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return await GenerateTokensAsync(user);
        }

        private async Task<AuthResponse> GenerateTokensAsync(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(30);

            var jwt = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow,
                IsRevoked = false,
            };

            await _tokenRepo.AddAsync(refreshToken);
            await _tokenRepo.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = expires
            };
        }
    }
}
