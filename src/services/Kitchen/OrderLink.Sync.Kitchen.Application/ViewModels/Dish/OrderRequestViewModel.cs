using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Application.ViewModels.Dish
{
    public class OrderRequestViewModel
    {
        public List<Guid> Dishes { get; set; }
        public string Note { get; set; }

        public Order ToEntity(Guid id)
        {
            return new Order(id, Note);
        }
    }
}
