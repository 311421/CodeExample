using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class ProductType
    {
        private string _name;
        public ProductType(string name)
        {
            _name = name ?? throw new ShopException("Incorrect name");
        }

        public string Name
        {
            get => new string(_name);
        }
    }
}