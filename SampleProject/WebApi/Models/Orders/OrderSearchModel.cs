using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Orders
{
    public class OrderSearchModel
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Tag { get; set; }
    }
}