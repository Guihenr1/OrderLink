using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Kitchen.Domain.Entities
{
    public class Order : EntityBase
    {
        public string Note { get; private set; }

        public virtual ICollection<OrderDish> OrderDishes { get; set; }

        public Order(string note)
        {
            Note = note;
        }
    }   
}
