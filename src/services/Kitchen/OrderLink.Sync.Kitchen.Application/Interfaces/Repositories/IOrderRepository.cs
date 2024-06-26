﻿using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersWithOrderDishesAsync();
    }
}
