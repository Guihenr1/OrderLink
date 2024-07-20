namespace OrderLink.Sync.Order.Application.ViewModels
{
    public class OrderResponseViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public bool Done { get; set; }
    }
}