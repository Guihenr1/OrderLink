using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.Services;
using OrderLink.Sync.Kitchen.Infrastructure.Context;
using OrderLink.Sync.Kitchen.Infrastructure.Repositories;

namespace OrderLink.Sync.Kitchen.Api.Configurations
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
            services.AddScoped<INotificator, Notificator>();
        }

        private static void Services(this IServiceCollection services)
        {
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDishService, OrderDishService>();
            services.AddScoped<IInvokeService, InvokeService>();
        }

        private static void Repositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDishRepository, OrderDishRepository>();
            services.AddScoped<IConsumerOrderService, ConsumerOrderService>();
        }
    }
}
