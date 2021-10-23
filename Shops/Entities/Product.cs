using System;
using System.Collections.ObjectModel;
using Shops.Tools;

namespace Shops.Entities
{
    public struct Product
    {
        private ProductType _productType;
        private uint _amount;
        public Product(ProductType productType, uint amount)
        {
            _productType = productType ?? throw new ShopException("Incorrect product type");
            _amount = amount;
        }

        public string ProductName => _productType.Name;
    }
}