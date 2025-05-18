using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid Customer { get; set; }
        public IEnumerable<Guid> Products { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}