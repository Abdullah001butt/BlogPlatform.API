using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPlatform.Context.DependencyInjection
{
    public static class ContextServiceRegistration
    {
        public static IServiceCollection AddContextRegistration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
