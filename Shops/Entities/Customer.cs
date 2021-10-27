using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private float _money;
        private string _name;
        public Customer(string name, float money)
        {
            _money = money;
            _name = name;
        }

        public float Money
        {
            get
            {
                return _money;
            }
            set
            {
                if (value < 0)
                {
                    throw new ShopException("Invalid customer money");
                }

                _money = value;
            }
        }
    }
}