using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id, bool doInclude = false);

        User GetOrderUser(Guid id);

        IEnumerable<Product> GetOrderProducts(IEnumerable<Guid> ids);
        IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? productId = null,DateTime? OrderDate = null, string tag = null);
    }
}