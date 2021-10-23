using System;
using System.Collections.Generic;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private Dictionary<string, uint> _products = new Dictionary<string, uint>();
        private string _name;
        private string _address;

        public Shop(string name, string address, ShopData shopData = null)
        {
            _name = name ?? throw new ShopException("Incorrect name");
            _address = address ?? throw new ShopException("Incorrect address");
            if (shopData == null)
            {
                ShopData.DefaultData.AddShop(this);
            }
            else
            {
                shopData.AddShop(this);
            }
        }

        public void AddProduct(ProductType productType, uint amount)
        {
            if (productType == null) throw new ShopException("Incorrect product");
            if (_products.ContainsKey(productType.Name))
            {
                _products[productType.Name] += amount;
            }

            _products.Add(productType.Name, amount);
        }

        public void RemoveProduct(ProductType productType, uint amount)
        {
            if (_products[productType.Name] < amount)
            {
                throw new ShopException("Reduced value is lower than parameter");
            }

            _products[productType.Name] -= amount;
        }


    }
}