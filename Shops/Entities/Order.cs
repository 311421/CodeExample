using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops.Entities
{
    public struct Order
    {
        private List<Product> _orderList;
        private Customer _customer;
        public Order(List<Product> orderList, Customer customer)
        {
            _orderList = orderList;
            _customer = customer;
        }

        public ReadOnlyCollection<Product> OrderList => _orderList.AsReadOnly();
    }
}