﻿using OrderLink.Sync.Core.Data;

namespace OrderLink.Sync.Order.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Domain.Entities.Order>
    {
        Task<Domain.Entities.Order> GetByOrderIdAsync(Guid orderId);
    }
}
