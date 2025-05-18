using BusinessEntities;
using Core.Services.Orders;
using Raven.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order != null)
            {
                return AlreadyExist(string.Format("Order for custommer '{0}' already exist with id '{1}'. Consider changing the ID or Delete the existing order and attempt again", order.Customer, order.Id));
            }
            try
            {
                order = _createOrderService.Create(orderId, model.Customer, model.Products, model.Quantity, model.OrderDate, model.Amount, model.Tags);
            }
            catch (Exception ex)
            {
                return InvalidData(ex.Message);
            }
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            if (!ModelState.IsValid)
            {
                var modelValidationErrors = String.Join(":", ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)));
                return InvalidData(string.Format("Updating Order '{0}' Failed with validation Error(s) '{1}'", order.Id, modelValidationErrors));
            }
            _updateOrderService.Update(order, model.Customer, model.Products, model.Quantity, model.OrderDate, model.Amount, model.Tags);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);
            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/{include}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId, bool? include)
        {
            var order = _getOrderService.GetOrder(orderId, include.HasValue && include.Value);
            if (order == null)
            {
                return DoesNotExist();
            }
            return include.HasValue && include.Value ? Found(new OrderData(order, _getOrderService.GetOrderUser(order.Customer), _getOrderService.GetOrderProducts(order.Products))) : Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders([FromBody] OrderSearchModel searchModel)
        {
            return GetOrders(searchModel, null, null);
        }

        [Route("list/{skip}")]
        [HttpGet]
        public HttpResponseMessage GetOrders([FromBody] OrderSearchModel searchModel, int? skip)
        {
            return GetOrders(searchModel, skip, null);
        }

        [Route("list/{skip}/{take}")]
        [HttpGet]
        public HttpResponseMessage GetOrders([FromBody] OrderSearchModel searchModel, int? skip, int? take)
        {
            //As per RevenDB, queries will return up to 1024 results due to the server default max page size value.
            var orders = _getOrderService.GetOrders(searchModel.CustomerId, searchModel.ProductId, searchModel.OrderDate, searchModel.Tag)
                                       .Skip(skip.HasValue ? skip.Value : 0).Take(take.HasValue ? take.Value : 9999)
                                       .Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }

       
        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            _deleteOrderService.DeleteAll();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetOrdersByTag(string tag, int? skip, int? take)
        {
            return this.GetOrders( new OrderSearchModel() { Tag = tag},skip,take);
        }
    }
}