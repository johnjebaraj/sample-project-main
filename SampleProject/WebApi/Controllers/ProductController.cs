using BusinessEntities;
using Core.Services.Products;
using Raven.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid userId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(userId);
            if (product != null)
            {
                return AlreadyExist(string.Format("Product '{0}' already exist with id '{1}'. Consider changing the ID or Delete the existing product and attempt again", product.Name, product.Id));
            }
            product = _createProductService.Create(userId, model.Name, model.Description, model.Type, model.Date, model.Price, model.Tags);
            return Found(new ProductData(product));
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid userId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(userId);
            if (product == null)
            {
                return DoesNotExist();
            }
            if (!ModelState.IsValid)
            {
                var modelValidationErrors = String.Join(":", ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)));
                return InvalidData(string.Format("Updating Product '{0}' Failed with validation Error(s) '{1}'", product.Name, modelValidationErrors));
            }
            _updateProductService.Update(product, model.Name, model.Description, model.Type, model.Date, model.Price, model.Tags);
            return Found(new ProductData(product));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid userId)
        {
            var product = _getProductService.GetProduct(userId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(product);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid userId)
        {
            var product = _getProductService.GetProduct(userId);
            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts([FromBody] ProductSearchModel searchModel)
        {
            return GetProducts(searchModel, null, null);
        }

        [Route("list/{skip}")]
        [HttpGet]
        public HttpResponseMessage GetProducts([FromBody] ProductSearchModel searchModel, int? skip)
        {
            return GetProducts(searchModel, skip, null);
        }

        [Route("list/{skip}/{take}")]
        [HttpGet]
        public HttpResponseMessage GetProducts([FromBody] ProductSearchModel searchModel, int? skip, int? take)
        {
            //As per RevenDB, queries will return up to 1024 results due to the server default max page size value.
            var products = _getProductService.GetProducts(searchModel.Type, searchModel.Name, searchModel.Tag)
                                       .Skip(skip.HasValue ? skip.Value : 0).Take(take.HasValue ? take.Value : 9999)
                                       .Select(q => new ProductData(q))
                                       .ToList();
            return Found(products);
        }

       
        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _deleteProductService.DeleteAll();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetProductsByTag(string tag, int? skip, int? take)
        {
            return this.GetProducts( new ProductSearchModel() { Tag = tag},skip,take);
        }
    }
}