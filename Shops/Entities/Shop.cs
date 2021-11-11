using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        private string _name;
        private string _address;

        public Shop(string name, string address, ShopData shopData)
        {
            _name = name ?? throw new ShopException("Incorrect name");
            _address = address ?? throw new ShopException("Incorrect address");
            shopData.AddShop(this);
        }

        public Shop(string name, string address)
        {
            _name = name ?? throw new ShopException("Incorrect name");
            _address = address ?? throw new ShopException("Incorrect address");
            ShopData.DefaultData.AddShop(this);
        }

        public float ProductPrice(ProductType productType)
        {
            return _products[productType.Name].Price;
        }

        public float ProductAmount(ProductType productType)
        {
            return _products[productType.Name].Amount;
        }

        public void ChangeProductPrice(ProductType productType, float targetValue)
        {
            _products[productType.Name].Price = targetValue;
        }

        public void AddProduct(ProductType productType, uint amount, uint price)
        {
            if (productType == null) throw new ShopException("Incorrect product");
            if (_products.ContainsKey(productType.Name))
            {
                _products[productType.Name].Amount += amount;
                _products[productType.Name].Price = price;
            }
            else
            {
                _products.Add(productType.Name, new Product(productType, amount, price));
            }
        }

        public void RemoveProduct(string product, uint amount)
        {
            if (_products[product].Amount < amount)
            {
                throw new ShopException("Reduced value is lower than parameter");
            }

            _products[product].Amount -= amount;
        }

        public float? OrderPrice(Order order)
        {
            float? totalPrice = 0;
            foreach (float? curPrice in order.OrderList.Select(request => PriceOf(request)))
            {
                if (curPrice != null)
                {
                    totalPrice += curPrice;
                }
                else
                {
                    return null;
                }
            }

            return totalPrice;
        }

        private float? PriceOf(Request request)
        {
            if (request == null) throw new ShopException("Incorrect order");
            Product product = _products[request.ProductName];
            if (product.Amount < request.Amount)
            {
                return null;
            }

            return product.Price * request.Amount;
        }
    }
}