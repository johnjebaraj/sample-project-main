using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private string _name;
        private string _description;
        private ProductTypes _type;
        private DateTime? _date = null;
        private decimal _price;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public ProductTypes Type
        {
            get => _type;
            private set => _type = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public DateTime? Date
        {
            get => _date;
            private set => _date = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }
     
        public void SetType(ProductTypes type)
        {
            _type = type;
        }

        public void SetPrice(decimal price)
        {
            _price = price;
        }

        public void SetDate(DateTime? date)
        {
            _date = date;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}