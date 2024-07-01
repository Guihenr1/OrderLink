using OrderLink.Sync.MessageBus;

namespace OrderLink.Sync.Order.Api.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("MessageBus"));
        }
    }
}
