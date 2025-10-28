using BlogPlatform.Services.Implementations;
using BlogPlatform.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPlatform.Services.DependencyInjection
{
    public static class ServiceLayerRegistration
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
