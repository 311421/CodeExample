using System;
using System.Collections.ObjectModel;
using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        private ProductType _productType;
        private uint _amount;
        private uint _price;
        public Product(ProductType productType, uint amount, uint price)
        {
            _productType = productType ?? throw new ShopException("Incorrect product type");
            _amount = amount;
            _price = price;
        }

        public string ProductName => _productType.Name;

        public uint Amount
        {
            get => _amount;
            set
            {
                if (value <= 0)
                {
                    throw new ShopException("Incorrect amount");
                }

                _amount = value;
            }
        }

        public uint Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ShopException("Incorrect price");
                }

                _price = value;
            }
        }
    }
}