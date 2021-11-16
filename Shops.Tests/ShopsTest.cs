using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;

namespace Shops.Tests
{
    public class Tests
    {
        private Shop _shop1;
        private ProductType product1;
        private Customer _customer1;
        private List<Request> _requestList;

        [SetUp]
        public void Setup()
        {
            product1 = ShopData.DefaultData.RegisterProduct("Banana");
            _shop1 = new Shop("Grocery", "Something-something");
        }

        [Test]
        public void ProductAddAndBuy()
        {
            // Customer config
            _customer1 = new Customer("Egor", 50);
            float startingMoney = _customer1.Money;
            // Shop config
            _shop1.AddProduct(product1, 5, 10);
            // Order config
            var request = new Request(product1, 5);
            var requestList = new List<Request> {request};
            var order = new Order(requestList, _customer1);
            //
            order.ExecuteOrder(_shop1);
            Assert.AreEqual(0, _customer1.Money);
        }

        [Test]
        public void ChangeProductPrice()
        {
            _shop1.AddProduct(product1, 1, 10);
            _shop1.ChangeProductPrice(product1, 20);
            Assert.AreEqual(20, _shop1.ProductPrice(product1));
        }

        [Test]
        public void BuyCheapestBatchSuccess()
        {
            _customer1 = new Customer("Egor", 50);
            //Product2 config
            var product2 = new ProductType("Apple");
            //Shop1 config
            _shop1.AddProduct(product1, 5, 10);
            _shop1.AddProduct(product2, 10, 7);
            //Shop2 config
            var shop2 = new Shop("TestTest", "Whatever");
            shop2.AddProduct(product1, 7, 9);
            shop2.AddProduct(product2, 8, 8);
            //Request config
            var request1 = new Request(product1, 5);
            var request2 = new Request(product2, 4);
            var requestList = new List<Request> {request1, request2};
            var order = new Order(requestList, _customer1);
            //
            Shop bestShop = order.FindBestShop();
            if (bestShop == _shop1)
            {
                Assert.LessOrEqual((float) bestShop.OrderPrice(order), (float) shop2.OrderPrice(order));
            }
            else
            {
                Assert.LessOrEqual((float) bestShop.OrderPrice(order), (float) _shop1.OrderPrice(order));
            }
        }
        
        [Test]
        public void BuyCheapestBatchFailure()
        {
            _customer1 = new Customer("Egor", 50);
            //Product2 config
            var product2 = new ProductType("Apple");
            //Shop1 config
            _shop1.AddProduct(product1, 5, 10);
            _shop1.AddProduct(product2, 10, 7);
            //Shop2 config
            var shop2 = new Shop("TestTest", "Whatever");
            shop2.AddProduct(product1, 7, 9);
            shop2.AddProduct(product2, 8, 8);
            //Request config
            var request1 = new Request(product1, 6);
            var request2 = new Request(product2, 9);
            var requestList = new List<Request> {request1, request2};
            var order = new Order(requestList, _customer1);
            //
            Shop bestShop = order.FindBestShop();
            Assert.AreEqual(null, bestShop);
        }
        
        [Test]
        [TestCase(7U, 10U, 10U, 7U, 6U, 9U, 
            200U)]
        public void BuyFromShop(uint product1Amount, uint product1Price, uint product2Amount, uint product2Price,
            uint requestedAmount1, uint requestedAmount2, uint customerMoney)
        {
            // Customer
            _customer1 = new Customer("Egor", customerMoney);
            // Product2 config
            var product2 = new ProductType("Apple");
            // Shop1 config
            _shop1.AddProduct(product1, product1Amount, product1Price);
            _shop1.AddProduct(product2, product2Amount, product2Price);
            // Request config
            var request1 = new Request(product1, requestedAmount1);
            var request2 = new Request(product2, requestedAmount2);
            var requestList = new List<Request> {request1, request2};
            var order = new Order(requestList, _customer1);
            //
            order.ExecuteOrder(_shop1);
            Assert.AreEqual(_shop1.ProductAmount(product1), product1Amount-requestedAmount1);
            Assert.AreEqual(_shop1.ProductAmount(product2), product2Amount-requestedAmount2);
            Assert.AreEqual(_customer1.Money, customerMoney-product1Price*requestedAmount1-
                                              product2Price*requestedAmount2);
        }
    }
}