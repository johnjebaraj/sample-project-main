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
        private List<Guid> _products;
        private decimal _total;
        private string _address;
        private DateTime _date;

        public Guid Customer
        {
            get => _customer;
            private set => _customer = value;
        }

        public string Address
        {
            get => _address;
            private set => _address = value;
        }

        public decimal Total
        {
            get => _total;
            private set => _total = value;
        }

        public DateTime Date
        {
            get => _date;
            private set => _date = value;
        }

        public IEnumerable<Guid> Products
        {
            get => _products;
            private set => _products.Initialize(value);
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
        public void SetDate(DateTime date)
        {
            _date = date;
        }

        public void SetTotal(Decimal total)
        {
            _total = total;
        }


        public void SetAddress(string address)
        {
            _address = address;
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