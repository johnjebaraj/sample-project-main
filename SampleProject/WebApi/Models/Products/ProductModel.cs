using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypes Type { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<string> Tags { get; set; }

        private static List<ProductModel> _fabricatedModels = null;
        public static List<ProductModel> GetFabiricatedProducts()
        {
            if(_fabricatedModels is null)
            {
                Random rand = new Random();
                _fabricatedModels = new List<ProductModel>();
                var numbers = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight","Nine","Ten" };
                foreach (var x in numbers)
                {
                    _fabricatedModels.Add(new ProductModel() { Name = "Product_" + x, Price = 10.99M * rand.Next(10), Description = "Product Price -" + x, Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                    _fabricatedModels.Add(new ProductModel() { Name = "Service_" + x, Price = 10.99M * rand.Next(10), Description = "Service Price -" + x, Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                }
                /*_fabricatedModels.Add(new ProductModel() { Name = "ProductOne", Price = 10.99M, Description = "Product One Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service One", Price = 10.99M, Description = "Service One Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductTwo", Price = 10.99M, Description = "Product Two Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Two", Price = 10.99M, Description = "Service Two Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductThree", Price = 10.99M, Description = "Product Three Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Three", Price = 10.99M, Description = "Service Three Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductFour", Price = 10.99M, Description = "Product Four Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Four", Price = 10.99M, Description = "Service Four Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductFive", Price = 10.99M, Description = "Product Five Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Five", Price = 10.99M, Description = "Service Five Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductSix", Price = 10.99M, Description = "Product Six Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Six", Price = 10.99M, Description = "Service Six Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductSeven", Price = 10.99M, Description = "Product Seven Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Seven", Price = 10.99M, Description = "Service Seven Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductEight", Price = 10.99M, Description = "Product Eight Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Eight", Price = 10.99M, Description = "Service Eight Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductNine", Price = 10.99M, Description = "Product Nine Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Nine", Price = 10.99M, Description = "Service Nine Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });
                _fabricatedModels.Add(new ProductModel() { Name = "ProductTen", Price = 10.99M, Description = "Product Ten Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Commodity });
                _fabricatedModels.Add(new ProductModel() { Name = "Service Ten", Price = 10.99M, Description = "Service Ten Price ", Date = DateTime.Today.AddDays(rand.Next(100) * -1), Type = ProductTypes.Service });*/
            }
            return _fabricatedModels;

        }
    }
}