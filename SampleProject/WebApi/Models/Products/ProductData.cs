using BusinessEntities;
using System;
using System.Diagnostics;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Description = product.Description;
            Name = product.Name;
            Type = new EnumData(product.Type);
            Date = product.Date;
            Price = product.Price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public EnumData Type { get; set; }
        public DateTime? Date { get; set; }
        public Decimal Price { get; set; }
    }
}