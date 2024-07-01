using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.MessageBus;

namespace OrderLink.Sync.Kitchen.Application.Handlers
{
    public class OrderHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public OrderHandler(IServiceProvider serviceProvider,
                                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.SubscribeAsync<CreateOrderEvent>("CreateOrderHandler", async request =>
                await CreateOrderAsync(request));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> CreateOrderAsync(CreateOrderEvent message)
        {
            if (_bus.IsConnected is false) return new ValidationResult();

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IConsumerOrderService>();
            await service.CreateOrderAsync(message);

            return new ValidationResult();
        }
    }
}
