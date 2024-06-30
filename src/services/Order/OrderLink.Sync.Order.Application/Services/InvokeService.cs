using Microsoft.Extensions.Logging;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.MessageBus;
using OrderLink.Sync.Order.Application.Interfaces.Services;

namespace OrderLink.Sync.Order.Application.Services
{
    public class InvokeService : IInvokeService
    {
        private readonly IMessageBus _bus;
        private readonly ILogger<InvokeService> _logger;

        public InvokeService(IMessageBus bus, ILogger<InvokeService> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task CreateOrderAsync(CreateOrderEvent createOrderEvent)
        {
            _logger.LogInformation("Creating order event");

            await _bus.PublishAsync(createOrderEvent);

            _logger.LogInformation("Order event created");
        }
    }
}
