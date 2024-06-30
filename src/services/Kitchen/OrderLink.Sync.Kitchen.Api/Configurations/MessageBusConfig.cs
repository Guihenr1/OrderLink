using OrderLink.Sync.Kitchen.Application.Handlers;
using OrderLink.Sync.MessageBus;

namespace OrderLink.Sync.Kitchen.Api.Configurations
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
