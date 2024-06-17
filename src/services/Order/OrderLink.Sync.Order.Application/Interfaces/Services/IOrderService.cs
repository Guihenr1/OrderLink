﻿using OrderLink.Sync.Order.Application.ViewModels;

namespace OrderLink.Sync.Order.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task AddAsync(OrderRequestViewModel orderViewModel);

        Task<IEnumerable<DishViewModelData>> GetAllDishesAsync();
    }
}