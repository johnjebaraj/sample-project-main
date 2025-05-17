using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description, ProductTypes type, DateTime? date, decimal price, IEnumerable<string> tags);
    }
}