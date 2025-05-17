using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Product Create(Guid id, string name, string description, ProductTypes type, DateTime? date,Decimal price, IEnumerable<string> tags);
    }
}