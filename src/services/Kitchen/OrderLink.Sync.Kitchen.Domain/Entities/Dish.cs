using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Kitchen.Domain.Entities
{
    public class Dish : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<OrderDish> OrderDishes { get; set; }

        public Dish(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Dish(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
