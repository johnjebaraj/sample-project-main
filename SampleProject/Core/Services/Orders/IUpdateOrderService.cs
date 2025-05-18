using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid customerId, IEnumerable<Guid> productIs, int quantity, DateTime orderDate, Decimal amount, IEnumerable<string> tags);
    }
}