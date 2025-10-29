using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPlatform.Context.DependencyInjection
{
    public static class ContextServiceRegistration
    {
        public static IServiceCollection AddContextRegistration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlogDbContext>(options =>
            {
                var connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

                // Acquire access token using Managed Identity
                var credential = new DefaultAzureCredential();
                var token = credential.GetToken(
                    new TokenRequestContext(new[] { "https://database.windows.net/.default" }));

                connection.AccessToken = token.Token;

                options.UseSqlServer(connection);
            });

            return services;
        }
    }
}