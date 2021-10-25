using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;

namespace Shops.Services
{
    public class ShopData
    {
        private List<Shop> _shops = new List<Shop>();
        private HashSet<ProductType> _productTypes = new HashSet<ProductType>();
        public static ShopData DefaultData { get; } = new ();

        public ProductType RegisterProduct(string name)
        {
            var newProduct = new ProductType(name);
            _productTypes.Add(newProduct);
            return newProduct;
        }

        public void AddShop(Shop shop)
        {
            _shops.Add(shop);
        }

        public List<Shop> ShopList => new List<Shop>(_shops);
    }
}