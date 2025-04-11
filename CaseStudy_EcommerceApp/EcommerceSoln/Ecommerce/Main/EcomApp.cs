using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model;
using Ecommerce.Dao;
using Ecommerce.Exceptions;
using Ecommerce.Util;
using System.Data.SqlClient;


namespace Ecommerce.Main
{
    public class EcomApp
    {
        public static void Main(string[] args)
        {
            OrderProcessorRepository repo = new OrderProcessorRepositoryImpl();
            bool running = true;

            while (running)
            {
                try
                {
                    Console.WriteLine(" ECOMMERCE MENU\n ");
                    Console.WriteLine("1. Register Customer");
                    Console.WriteLine("2. Create Product");
                    Console.WriteLine("3. Delete Product");
                    Console.WriteLine("4. Add to Cart");
                    Console.WriteLine("5. View Cart");
                    Console.WriteLine("6. Place Order");
                    Console.WriteLine("7. View Customer Orders");
                    Console.WriteLine("8. Exit\n");
                    Console.WriteLine("Additional features\n");
                    Console.WriteLine("9. Update Customer");
                    Console.WriteLine("10. Remove Product from Cart");
                    Console.WriteLine("11. View All Products\n");

                    Console.Write("Enter choice: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Customer Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Password: ");
                            string password = Console.ReadLine();

                            Customer newCustomer = new Customer { Name = name, Email = email, Password = password };
                            Console.WriteLine(repo.createCustomer(newCustomer) ? "Customer Registered." : "Failed.");
                            break;

                        case 2:
                            Console.Write("Product Name: ");
                            string pname = Console.ReadLine();
                            Console.Write("Price: ");
                            decimal price = decimal.Parse(Console.ReadLine());
                            Console.Write("Description: ");
                            string desc = Console.ReadLine();
                            Console.Write("Stock: ");
                            int stock = int.Parse(Console.ReadLine());

                            Product newProduct = new Product { Name = pname, Price = price, Description = desc, StockQuantity = stock };
                            Console.WriteLine(repo.createProduct(newProduct) ? "Product Created." : "Failed.");
                            break;

                        case 3:
                            Console.Write("Enter Product ID: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine(repo.deleteProduct(pid) ? "Deleted." : "Not Found.");
                            break;

                        case 4:
                            Console.Write("Customer ID: ");
                            int custId = int.Parse(Console.ReadLine());
                            Console.Write("Product ID: ");
                            int prodId = int.Parse(Console.ReadLine());
                            Console.Write("Quantity: ");
                            int qty = int.Parse(Console.ReadLine());

                            Customer cust = new Customer { CustomerId = custId };
                            Product prod = new Product { ProductId = prodId };

                            Console.WriteLine(repo.addToCart(cust, prod, qty) ? "Added to cart." : "Failed.");
                            break;

                        case 5:
                            Console.Write("Customer ID: ");
                            int custIdView = int.Parse(Console.ReadLine());
                            var customer = new Customer { CustomerId = custIdView };

                            List<Product> cart = repo.getAllFromCart(customer);
                            if (cart.Count == 0)
                                Console.WriteLine("Cart is empty.");
                            else
                            {
                                Console.WriteLine("Cart Items:");
                                foreach (var p in cart)
                                    Console.WriteLine($"Product: {p.Name} , Price: {p.Price} , Qty: {p.StockQuantity}");
                            }
                            break;

                        case 6:
                            Console.Write("Customer ID: ");
                            int customerId = int.Parse(Console.ReadLine());
                            Console.Write("Shipping Address: ");
                            string addr = Console.ReadLine();

                            var customerObj = new Customer { CustomerId = customerId };
                            List<Product> cartItems = repo.getAllFromCart(customerObj);

                            if (cartItems.Count == 0)
                            {
                                Console.WriteLine("Cart is empty.");
                                return;
                            }

                            Dictionary<Product, int> orderItems = new Dictionary<Product, int>();

                            foreach (var item in cartItems)
                            {
                                string checkStock = "select stockQuantity from products where product_id = @prodId";
                                SqlConnection con =DBConnection.GetConnection();
                                using (SqlCommand cmdStock = new SqlCommand(checkStock, con))
                                {
                                    cmdStock.Parameters.AddWithValue("@prodId", item.ProductId);
                                    con.Open();
                                    int stockQuantity = (int)cmdStock.ExecuteScalar();
                                    con.Close();

                                    Console.WriteLine($"Available stock for {item.Name}: {stockQuantity}");

                                    int orderQty;
                                    do
                                    {
                                        Console.Write($"Enter quantity for {item.Name} : ");
                                        orderQty = int.Parse(Console.ReadLine());

                                        if (orderQty > stockQuantity)
                                        {
                                            Console.WriteLine("Enter a valid quantity.");
                                        }
                                        else if (orderQty < 0)
                                        {
                                            Console.WriteLine("Please enter a valid quantity.");
                                        }
                                    } while (orderQty > stockQuantity || orderQty < 0); 

                                    orderItems[item] = orderQty;
                                }
                            }

                            Console.WriteLine(repo.placeOrder(customerObj, orderItems, addr) ? "Order placed." : "Order failed.");
                            break;

                        case 7:
                            Console.Write("Enter Customer ID to view orders: ");
                            int custOrderId = int.Parse(Console.ReadLine());

                            Dictionary<Product, int> orders = repo.getOrdersByCustomer(custOrderId);
                            if (orders.Count == 0)
                                Console.WriteLine("No orders found.");
                            else
                            {
                                Console.WriteLine("Order Details:");
                                foreach (var entry in orders)
                                {
                                    Console.WriteLine($"{entry.Key.Name} x{entry.Value} | Price: {entry.Key.Price}");
                                }
                            }
                            break;


                        case 8:
                            running = false;
                            break;

                        case 9:
                            Console.Write("Customer ID: ");
                            int updateId = int.Parse(Console.ReadLine());
                            Console.Write("New Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("New Email: ");
                            string newEmail = Console.ReadLine();
                            Console.Write("New Password: ");
                            string newPass = Console.ReadLine();
                            Customer updatedCust = new Customer
                            {
                                CustomerId = updateId,
                                Name = newName,
                                Email = newEmail,
                                Password = newPass
                            };
                            Console.WriteLine(repo.updateCustomer(updatedCust) ? "Customer updated." : "Update failed.");
                            break;

                        case 10:
                            Console.Write("Customer ID: ");
                            int rcustId = int.Parse(Console.ReadLine());
                            Console.Write("Product ID to remove: ");
                            int rprodId = int.Parse(Console.ReadLine());
                            Customer rcust = new Customer { CustomerId = rcustId };
                            Product rprod = new Product { ProductId = rprodId };
                            Console.WriteLine(repo.removeFromCart(rcust, rprod) ? "Removed from cart." : "Failed.");
                            break;

                        case 11:
                            List<Product> products = repo.getAllProducts();
                            if (products.Count == 0)
                                Console.WriteLine("No products available.");
                            else
                            {
                                Console.WriteLine("Available Products:");
                                foreach (var p in products)
                                {
                                    Console.WriteLine($"ID: {p.ProductId}, Name: {p.Name}, Price: {p.Price}, Stock: {p.StockQuantity}, Description: {p.Description}");
                                }
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;

                    }
                }
                
                catch (CustomerNotFoundException ex)
                {
                Console.WriteLine("Customer Error: " + ex.Message);
                }
                catch (ProductNotFoundException ex)
                {
                Console.WriteLine("Product Error: " + ex.Message);
                }
                catch (OrderNotFoundException ex)
                {
                Console.WriteLine("Order Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                Console.WriteLine("Something went wrong: " + ex.Message);
                }
            }
        }
    }
}

