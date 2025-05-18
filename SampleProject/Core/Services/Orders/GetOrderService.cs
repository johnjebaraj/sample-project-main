using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrder(Guid id, bool doInclude = false)
        {
            return _orderRepository.Get(id, doInclude);
        }

        public User GetOrderUser(Guid id)
        {
            return _orderRepository.GetOrderUser(id);
        }

        public IEnumerable<Product> GetOrderProducts(IEnumerable<Guid> ids)
        {
            return _orderRepository.GetOrderProducts(ids);
        }

        public IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? productId = null,DateTime? OrderDate = null, string tag = null)
        {
            return _orderRepository.Get(customerId, productId, OrderDate, tag);
        }
    }
}