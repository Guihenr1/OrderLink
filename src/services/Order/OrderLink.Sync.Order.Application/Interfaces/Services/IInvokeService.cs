﻿using OrderLink.Sync.Core.Messages.Integration.Events;

namespace OrderLink.Sync.Order.Application.Interfaces.Services
{
    public interface IInvokeService
    {
        Task CreateOrderAsync(CreateOrderEvent createOrderEvent);
    }
}
