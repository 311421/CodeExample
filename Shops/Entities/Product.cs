using Shops.Tools;

namespace Shops.Entities
{
    public struct Product
    {
        private ProductType _productType;
        private uint _amount;
        public Product(ProductType productType, uint amount)
        {
            _productType = productType;
            _amount = amount;
        }
    }
}