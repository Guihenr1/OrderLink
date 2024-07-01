namespace OrderLink.Sync.Kitchen.Application.ViewModels.Order
{
    public class GetAllOrderResponseViewModel
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetAllDishesResponseViewModel> Dishes { get; set; }
    }

    public class GetAllDishesResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
