using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.MessageBus;
using OrderLink.Sync.Order.Application.Interfaces.Services;

namespace OrderLink.Sync.Order.Application.Handlers
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
            _bus.SubscribeAsync<DoneOrderEvent>("DoneOrderHandler", async request =>
                await DoneOrderAsync(request));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> DoneOrderAsync(DoneOrderEvent message)
        {
            if (_bus.IsConnected is false) return new ValidationResult();

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IConsumerOrderService>();
            await service.DoneOrderAsync(message);

            return new ValidationResult();
        }
    }
}
