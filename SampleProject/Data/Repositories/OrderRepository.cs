using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public Order Get(Guid id,bool doInclude = false)
        {
           var order = doInclude ? _documentSession.Include<Order>(x => x.Customer).Load<Order>(id) 
                : _documentSession.Load<Order>(id);
           return order;
        }
        public User GetOrderUser(Guid id)
        {
            var usr =  _documentSession.Load<User>(id);
            return usr;
        }

        public IEnumerable<Product> GetOrderProducts(IEnumerable<Guid> ids)
        {
            //RavenDB is giving hard time loading multiple IDs with version 3.5 so looping'
            var orderProds = new List<Product>();
            foreach(var id in ids)
            {
                orderProds.Add(_documentSession.Load<Product>(id));
            }
           return orderProds;
        }

        public IEnumerable<Order> Get(Guid? customerId = null, Guid? productId = null, DateTime? orderDate = null, string tag = null, bool doInclude = false)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrdersListIndex>();

            var hasFirstParameter = false;
            if (customerId.HasValue && customerId.Value != Guid.Empty)
            {
                query = query.WhereEquals("Customer", (Guid)customerId.Value);
                hasFirstParameter = true;
            }

            if (productId.HasValue && productId.Value != Guid.Empty)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query.ContainsAny("Products", new[] { productId.Value.ToString() });
            }
            if (orderDate.HasValue && orderDate.Value != DateTime.MinValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereEquals("OrderDate", orderDate.Value);
            }

            if (tag != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query.ContainsAny("Tags", new[] { tag });
                //query = query.WhereEquals("Tag", tag);
            }
            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<OrdersListIndex>();
        }
    }
}