using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid customerId, IEnumerable<Guid> productIds, int quantity, DateTime orderDate, Decimal amount, IEnumerable<string> tags)
        {
            order.SetCustomer(customerId);
            order.SetProducts(productIds);
            order.SetQuantity(quantity);
            order.SetAmount(amount);
            order.SetOrderDate(orderDate);
            order.SetTags(tags);
        }
    }
}