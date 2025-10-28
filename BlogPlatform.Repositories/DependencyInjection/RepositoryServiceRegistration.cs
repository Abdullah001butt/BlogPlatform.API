using BlogPlatform.Repositories.Implementations;
using BlogPlatform.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPlatform.Repositories.DependencyInjection
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
