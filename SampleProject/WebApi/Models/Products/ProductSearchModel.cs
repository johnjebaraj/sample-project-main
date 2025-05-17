using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Products
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public ProductTypes? Type { get; set; }
        public string Tag { get; set; }
    }
}