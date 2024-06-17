using OrderLink.Sync.Order.Application.Interfaces.Services;
using OrderLink.Sync.Order.Application.Services;
using System.Net.Http.Headers;
using Polly;
using OrderLink.Sync.Core.Extensions;

namespace OrderLink.Sync.Order.Api.Configuration
{
    public static class HttpFactoryConfig
    {
        public static IServiceCollection AddHttpFactoryConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var kitchenUrl = configuration["Urls:Kitchen"];
            if (string.IsNullOrEmpty(kitchenUrl))
            {
                throw new ArgumentNullException("Kitchen URL is missing in configuration");
            }

            services.AddHttpClient<IOrderService, OrderService>(nameof(OrderService), client =>
            {
                client.BaseAddress = new Uri(kitchenUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(s => s.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));

            return services;
        }
    }
}
