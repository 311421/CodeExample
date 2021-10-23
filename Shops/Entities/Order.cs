using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public struct Order
    {
        private List<Product> _orderList;
        private Customer _customer;
        private ShopData _shopData;
        public Order(List<Product> orderList, Customer customer, ShopData shopData = null)
        {
            _orderList = orderList ?? throw new ShopException("Invalid product list argument");
            _customer = customer ?? throw new ShopException("Invalid customer argument");
            _shopData = shopData ?? ShopData.DefaultData;
        }

        public List<Product> OrderList => new List<Product>(_orderList);
    }
}