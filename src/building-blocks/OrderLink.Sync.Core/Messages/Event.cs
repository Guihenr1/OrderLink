namespace OrderLink.Sync.Core.Messages
{
    public class Event : Message
    {
        public Guid TransactionId { get; private set; }
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.UtcNow;
            TransactionId = Guid.NewGuid();
        }
    }
}