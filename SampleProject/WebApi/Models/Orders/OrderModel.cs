using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypes Type { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}