namespace OrderLink.Sync.Kitchen.Application.ViewModels.Dish
{
    public class DishRequestViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        internal Domain.Entities.Dish ToEntity()
        {
            return new Domain.Entities.Dish(Name, Description);
        }
    }
}
