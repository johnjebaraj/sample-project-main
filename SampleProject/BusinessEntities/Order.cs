using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private Guid _customer;
        private List<Guid> _products = new List<Guid>();
        private decimal _amount;
        private int _quantity;
        private DateTime _orderDate;

        public Guid Customer
        {
            get => _customer;
            private set => _customer = value;
        }
        public IEnumerable<Guid> Products
        {
            get => _products;
            private set => _products.Initialize(value);
        }
        public Decimal Amount
        {
            get => _amount;
            private set => _amount = value;
        }
        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }
        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetCustomer(Guid customer)
        {
            _customer = customer;
        }
        public void SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
        }

        public void SetAmount(Decimal amount)
        {
            _amount = amount;
        }
        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
        }
        public void SetProducts(IEnumerable<Guid> products)
        {
            _products.Initialize(products);
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}