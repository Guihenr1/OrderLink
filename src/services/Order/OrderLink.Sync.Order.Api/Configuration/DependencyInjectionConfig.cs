using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Order.Application.Interfaces.Repositories;
using OrderLink.Sync.Order.Application.Interfaces.Services;
using OrderLink.Sync.Order.Application.Services;
using OrderLink.Sync.Order.Infrastructure.Repositories;

namespace OrderLink.Sync.Order.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Application(configuration);
            services.Repositories();
            services.Services();

            return services;
        }

        private static void Application(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpFactoryConfiguration(configuration);
            services.AddScoped<INotificator, Notificator>();
        }

        private static void Services(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        private static void Repositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
