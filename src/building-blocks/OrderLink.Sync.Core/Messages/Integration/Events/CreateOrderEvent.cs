using static OrderLink.Sync.Core.Models.AttributeBasedConventions;

namespace OrderLink.Sync.Core.Messages.Integration.Events
{
    [ExchangeName("CreateOrderEventExchange")]
    [QueueName("CreateOrderEventQueue")]
    public class CreateOrderEvent : IntegrationEvent
    {
        public CreateOrderEvent(Guid orderId, List<Guid> disheIds, string note)
        {
            OrderId = orderId;
            DisheIds = disheIds;
            Note = note;
        }

        public Guid OrderId { get; private set; }
        public List<Guid> DisheIds { get; private set; }
        public string Note { get; private set; }
    }
}
