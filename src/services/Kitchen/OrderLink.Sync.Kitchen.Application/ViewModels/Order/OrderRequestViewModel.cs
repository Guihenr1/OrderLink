namespace OrderLink.Sync.Kitchen.Application.ViewModels.Order
{
    public class OrderRequestViewModel
    {
        public Guid Id { get; set; }
        public List<Guid> Dishes { get; set; }
        public string Note { get; set; }

        public Domain.Entities.Order ToEntity()
        {
            return new Domain.Entities.Order(Id, Note);
        }
    }
}
