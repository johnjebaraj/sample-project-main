using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            Customer = order.Customer;
            Address = order.Address;
            Date = order.Date;
            Total = order.Total;
            Products = order.Products;
        }

        public Guid Customer { get; set; }
        public string Address { get; set; }
        public IEnumerable<Guid> Products { get; set; }
        public DateTime? Date { get; set; }
        public Decimal Total { get; set; }
    }
}