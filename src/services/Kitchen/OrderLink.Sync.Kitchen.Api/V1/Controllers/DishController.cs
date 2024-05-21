using Microsoft.AspNetCore.Mvc;
using OrderLink.Sync.Api.Core.Controllers;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;

namespace OrderLink.Sync.Kitchen.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/dish")]
    public class DishController : MainController
    {
        private readonly ILogger<DishController> _logger;
        private readonly IDishService _dishService;

        public DishController(
            INotificator notificator,
            ILogger<DishController> logger,
            IDishService dishService) : base(notificator)
        {
            _logger = logger;
            _dishService = dishService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DishRequestViewModel dishViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                await _dishService.AddAsync(dishViewModel);

                return CustomResponse();
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dishes = await _dishService.GetAllAsync();

                return CustomResponse(dishes);
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var dish = await _dishService.GetByIdAsync(id);

                return CustomResponse(dish);
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _dishService.RemoveAsync(id);

                return CustomResponse();
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DishRequestViewModel dishViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                await _dishService.UpdateAsync(dishViewModel, id);

                return CustomResponse();
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }
    }
}
