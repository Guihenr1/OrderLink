using Microsoft.Extensions.Logging;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.MessageBus;

namespace OrderLink.Sync.Kitchen.Application.Services
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

        public async Task DoneOrderAsync(DoneOrderEvent doneOrderEvent)
        {
            _logger.LogInformation("Done order event");

            await _bus.PublishAsync(doneOrderEvent);

            _logger.LogInformation("Done event created");
        }
    }
}
