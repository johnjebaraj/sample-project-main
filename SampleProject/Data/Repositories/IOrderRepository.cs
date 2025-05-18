using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Get(Guid id,bool doInclude = false);

        User GetOrderUser(Guid id);

        IEnumerable<Product> GetOrderProducts(IEnumerable<Guid> ids);

        IEnumerable<Order> Get(Guid? customerId = null, Guid? productId = null, DateTime? OrderDate = null, string tag = null, bool doInclude = false);
        void DeleteAll();
    }
}