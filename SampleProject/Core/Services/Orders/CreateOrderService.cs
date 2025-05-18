using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService, IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public Order Create(Guid id, Guid customerId, IEnumerable<Guid> productIds, int quantity, DateTime orderDate, Decimal amount, IEnumerable<string> tags)
        {

            Guid? invalidProdId = productIds is null ? default(Guid?)  : productIds.FirstOrDefault(p => _productRepository.Get(p) == null);
            
            if(invalidProdId.HasValue && invalidProdId.Value != Guid.Empty)
            {
                throw new InvalidOperationException(string.Format("Invalid product Id {0}", invalidProdId));
            }
            if (customerId != Guid.Empty && _userRepository.Get(customerId) is null)
            {
                throw new InvalidOperationException(string.Format("Invalid customer Id {0}", customerId));
            }
            var order = _orderFactory.Create(id);
            _updateOrderService.Update(order, customerId, productIds, quantity, orderDate, amount, tags);
            _orderRepository.Save(order);
            return order;
        }
    }
}