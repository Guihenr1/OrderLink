using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Kitchen.Infrastructure.Context;

namespace OrderLink.Sync.Kitchen.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KitchenDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            });

            return services;
        }
    }
}
