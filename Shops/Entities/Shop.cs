﻿using System.Collections.Generic;
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
            if (name == null || address == null)
            {
                throw new ShopException();
            }

            _name = name;
            _address = address;
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
            if (_products.ContainsKey(productType.Name))
            {
                _products[productType.Name] += amount;
            }

            _products.Add(productType.Name, amount);
        }
    }
}