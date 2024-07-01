using OrderLink.Sync.MessageBus;
using OrderLink.Sync.Order.Application.Handlers;

namespace OrderLink.Sync.Order.Api.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("MessageBus"));

            services.AddMessageBus(configuration.GetConnectionString("MessageBus"))
                .AddHostedService<OrderHandler>();
        }
    }
}
