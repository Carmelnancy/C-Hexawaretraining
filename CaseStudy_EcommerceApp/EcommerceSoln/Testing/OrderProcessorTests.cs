using Ecommerce.Dao;
using Ecommerce.Exceptions;
using Ecommerce.Model;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Ecommerce.Test
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private OrderProcessorRepositoryImpl repo;

        [SetUp]
        public void Setup()
        {
            repo = new OrderProcessorRepositoryImpl();
        }

        [Test]
        public void Test_ProductCreatedSuccessfully()
        {
            var product = new Product
            {
                Name = "Test Product",
                Price = 100,
                Description = "Testing",
                StockQuantity = 10
            };

            bool result = repo.createProduct(product);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_AddToCartSuccessfully()
        {
            var customer = new Customer { CustomerId = 1 }; 
            var product = new Product { ProductId =1 };     

            bool result = repo.addToCart(customer, product, 1);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_OrderPlacedSuccessfully()
        {
            var customer = new Customer { CustomerId = 1 }; 
            var product = new Product { ProductId = 1, Price = 99 }; 

            var orderItems = new Dictionary<Product, int>
            {
                { product, 2 }
            };

            bool result = repo.placeOrder(customer, orderItems, "Test Address");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_CustomerNotFoundExceptionThrown()
        {
            var invalidCustomer = new Customer { CustomerId = -1 }; 
            var product = new Product { ProductId = 1 };

            Assert.Throws<CustomerNotFoundException>(() =>
            {
                repo.addToCart(invalidCustomer, product, 1);
            });
        }

        [Test]
        public void Test_ProductNotFoundExceptionThrown()
        {
            var customer = new Customer { CustomerId = 1 };
            var invalidProduct = new Product { ProductId = -19 }; 

            Assert.Throws<ProductNotFoundException>(() =>
            {
                repo.addToCart(customer, invalidProduct, 1);
            });
        }
    }
}
