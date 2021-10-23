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
    }
}