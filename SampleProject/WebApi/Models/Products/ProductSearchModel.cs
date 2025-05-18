using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Orders;

namespace WebApi.Models.Products
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public ProductTypes? Type { get; set; }
        public string Tag { get; set; }
    }

    public class ProductFilterModel 
    {
        public string NameContains { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DescriptionContains { get; set; }
        public Decimal? FromPrice { get; set; }
        public Decimal? ToPrice { get; set; }

    }

}