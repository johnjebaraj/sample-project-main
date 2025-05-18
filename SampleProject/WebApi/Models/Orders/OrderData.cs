using BusinessEntities;
using Raven.Abstractions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApi.Models.Products;
using WebApi.Models.Users;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order,User user = null,IEnumerable<Product> products = null) : base(order)
        {
            Customer = user is null ? new UserData(new User()) { Id = order.Customer } : new UserData(user);
            Products = new List<ProductData>();
            AppendProducts(order, products);
            OrderDate = order.OrderDate;
            Quantity = order.Quantity;
            Amount = order.Amount;

        }
        private void AppendProducts(Order order,IEnumerable<Product> products = null)
        {
            //return fabricated Ordder products if nested level objects are not needed
            if (products is null)
            {
                foreach (var id in order.Products)
                {
                    Products.Add(new ProductData(new Product()) { Id = id });
                }
            }
            //return product object if nested level objects are needed
            else
            {
                foreach (var prod in products)
                {
                    Products.Add(new ProductData(prod));
                }
            }
        }
        public UserData Customer { get; set; }
        public int Quantity { get; set; }
        public List<ProductData> Products { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal Amount { get; set; }
    }
}