using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model;

namespace Ecommerce.Dao
{
    public interface OrderProcessorRepository
    {
        bool createProduct(Product product);
        bool createCustomer(Customer customer);
        bool deleteProduct(int productId);
        bool deleteCustomer(int customerId);
        bool addToCart(Customer customer, Product product, int quantity);
        bool removeFromCart(Customer customer, Product product);

        List<Product> getAllFromCart(Customer customer);

        bool placeOrder(Customer customer, Dictionary<Product, int> products, string shippingAddress);

        Dictionary<Product, int> getOrdersByCustomer(int customerId);
    }
}
