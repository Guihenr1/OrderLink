using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Order.Infrastructure.Context;

namespace OrderLink.Sync.Order.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            });

            return services;
        }
    }
}
