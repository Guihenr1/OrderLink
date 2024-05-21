using Microsoft.Extensions.Logging;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;
using OrderLink.Sync.Kitchen.Domain.Entities;
using OrderLink.Sync.Kitchen.Domain.Entities.Validations;

namespace OrderLink.Sync.Kitchen.Application.Services
{
    public class DishService : ServiceBase, IDishService
    {
        private readonly ILogger<DishService> _logger;
        private readonly IDishRepository _dishRepository;

        public DishService(ILogger<DishService> logger,
            INotificator notificator,
            IDishRepository dishRepository) : base(notificator)
        {
            _logger = logger;
            _dishRepository = dishRepository;
        }

        public async Task AddAsync(DishRequestViewModel dishViewModel)
        {
            _logger.LogInformation("Adding dish {DishName}", dishViewModel.Name);

            var dish = dishViewModel.ToEntity();

            if (!ExecuteValidation(new DishValidation(), dish))
            {
                _logger.LogWarning("Dish {DishName} is invalid", dishViewModel.Name);
                return;
            }

            await _dishRepository.AddAsync(dishViewModel.ToEntity());
            _logger.LogInformation("Dish {DishName} added", dishViewModel.Name);
        }

        public async Task<IEnumerable<DishResponseViewModel>> GetAllAsync()
        {
            _logger.LogInformation("Getting all dishes");
            var dishes = await _dishRepository.GetAllAsync();

            if (!dishes.Any())
            {
                _logger.LogWarning("No dishes found");
                return Enumerable.Empty<DishResponseViewModel>();
            }

            return dishes.Select(dish => new DishResponseViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                CreatedAt = dish.CreatedAt
            });
        }

        public async Task<DishResponseViewModel> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting dish {DishId}", id);
            var dish = await _dishRepository.GetByIdAsync(id);

            if (dish == null)
            {
                _logger.LogWarning("Dish {DishId} not found", id);
                return null;
            }

            return new DishResponseViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                CreatedAt = dish.CreatedAt
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            _logger.LogInformation("Removing dish {DishId}", id);
            var dish = await _dishRepository.GetByIdAsync(id);

            if (dish == null)
            {
                _logger.LogWarning("Dish {DishId} not found", id);
                return;
            }

            _dishRepository.Delete(dish);
        }

        public async Task UpdateAsync(DishRequestViewModel dishViewModel, Guid id)
        {
            _logger.LogInformation("Updating dish {DishId}", id);
            var checkExist = await _dishRepository.GetByIdAsync(id);
            if (checkExist == null)
            {
                _logger.LogWarning("Dish {DishId} not found", id);
                return;
            }

            var dish = new Dish(id, dishViewModel.Name, dishViewModel.Description);

            if (!ExecuteValidation(new DishValidation(), dish))
            {
                _logger.LogWarning("Dish {DishId} is invalid", id);
                return;
            }

            _dishRepository.Update(dish);
        }
    }
}
