using Shops.Tools;

namespace Shops.Entities
{
    public class Request
    {
        private ProductType _productType;
        private uint _amount;
        public Request(ProductType productType, uint amount, float price)
        {
            _productType = productType ?? throw new ShopException("Incorrect product type");
            _amount = amount;
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
    }
}