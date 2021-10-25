using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Order
    {
        private List<Request> _orderList;
        private Customer _customer;
        private ShopData _shopData;
        public Order(List<Request> orderList, Customer customer, ShopData shopData = null)
        {
            _orderList = orderList ?? throw new ShopException("Incorrect order list");
            _customer = customer ?? throw new ShopException("Incorrect customer");
            _shopData = shopData ?? ShopData.DefaultData;
        }

        public List<Request> OrderList => new List<Request>(_orderList);

        public Shop FindBestShop()
        {
            float? minPrice = float.MaxValue;
            Shop bestShop = null;
            foreach (Shop shop in _shopData.ShopList)
            {
                float? curPrice = shop.OrderPrice(this);
                if (minPrice < curPrice) continue;
                minPrice = curPrice;
                bestShop = shop;
            }

            return bestShop;
        }
    }
}