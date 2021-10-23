﻿using System;
using System.Collections.Generic;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
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

        public void AddProduct(ProductType productType, uint amount, uint price)
        {
            if (productType == null) throw new ShopException("Incorrect product");
            if (_products.ContainsKey(productType.Name))
            {
                _products[productType.Name].Amount += amount;
            }

            _products.Add(productType.Name, new Product(productType, amount, price));
        }

        public void RemoveProduct(ProductType productType, uint amount)
        {
            if (_products[productType.Name].Amount < amount)
            {
                throw new ShopException("Reduced value is lower than parameter");
            }

            _products[productType.Name].Amount -= amount;
        }
    }
}