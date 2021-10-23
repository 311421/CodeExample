using System.Collections.Generic;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private List<Product> _products = new List<Product>();
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
            _products.Add(new Product(productType, amount));
        }
    }
}