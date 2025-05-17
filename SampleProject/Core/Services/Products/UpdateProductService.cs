using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, ProductTypes type, DateTime? date, decimal price, IEnumerable<string> tags)
        {
            product.SetDescription(description);
            product.SetName(name);
            product.SetType(type);
            product.SetPrice(price);
            product.SetDate(date);
            product.SetTags(tags);
        }
    }
}