using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, Guid customerId, IEnumerable<Guid> productIds, int quantity, DateTime orderDate, Decimal amount, IEnumerable<string> tags);
    }
}
