using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IDishService
    {
        Task AddAsync(DishRequestViewModel dishViewModel);

        Task UpdateAsync(DishRequestViewModel dishViewModel, Guid id);

        Task RemoveAsync(Guid id);

        Task<IEnumerable<DishResponseViewModel>> GetAllAsync();

        Task<DishResponseViewModel> GetByIdAsync(Guid id);
    }
}
