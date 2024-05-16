using EasyNetQ.Interception;
using Microsoft.Extensions.DependencyInjection;

namespace OrderLink.Sync.MessageBus;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection))
        {
            ArgumentNullException argumentNullException = new();
            throw argumentNullException;
        }

        services.AddSingleton<IMessageBus>(new MessageBus(connection));
        services.AddSingleton<IProduceConsumeInterceptor, LoggingInterceptor>();

        return services;
    }
}