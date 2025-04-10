using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model;
using Ecommerce.Util;
using Ecommerce.Exceptions;

namespace Ecommerce.Dao
{
    public class OrderProcessorRepositoryImpl : OrderProcessorRepository
    {
        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;

        public bool createCustomer(Customer customer)
        {
            try
            {
                con=DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("insert into customers values (@name,@email,@password)",con);
                cmd.Parameters.AddWithValue("name",customer.Name);
                cmd.Parameters.AddWithValue("email",customer.Email);
                cmd.Parameters.AddWithValue("password",customer.Password);

                int count=cmd.ExecuteNonQuery();
                con.Close();
                return count > 0;

            }
            catch(SqlException e) {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        public bool createProduct(Product product)
        {
            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("insert into products values (@name,@price,@des,@stock)", con);
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("des", product.Description);
                cmd.Parameters.AddWithValue("stock", product.StockQuantity);

                int count = cmd.ExecuteNonQuery();
                con.Close();
                return count > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool deleteCustomer(int customerId)
        {
            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("delete from customers where customer_id=@id",con);
                cmd.Parameters.AddWithValue("id", customerId);
                int count = cmd.ExecuteNonQuery();
                con.Close();
                if (count > 0) {
                    return true;
                }
                else
                {
                    throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
                    return false;
                }
            }
            catch (SqlException e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool deleteProduct(int productId)
        {
            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("delete from products where product_id=@id", con);
                cmd.Parameters.AddWithValue("id", productId);
                int count = cmd.ExecuteNonQuery();
                con.Close();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    throw new ProductNotFoundException($"Product with ID {productId} not found.");
                    //return false;   
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool addToCart(Customer customer,Product product,int quantity)
        {
            try
            {
                con = DBConnection.GetConnection();
                con.Open();

                cmd = new SqlCommand("select count(*) from customers where customer_id = @id", con);
                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                int custCount = (int)cmd.ExecuteScalar();
                if (custCount == 0)
                    throw new CustomerNotFoundException($"Customer with ID {customer.CustomerId} not found.");

                cmd = new SqlCommand("select count(*) from products where product_id = @id", con);
                cmd.Parameters.AddWithValue("@id", product.ProductId);
                int prodCount = (int)cmd.ExecuteScalar();
                if (prodCount == 0)
                    throw new ProductNotFoundException($"Product with ID {product.ProductId} not found.");

                cmd = new SqlCommand("insert into cart values (@cid,@prid,@quan)", con);
                cmd.Parameters.AddWithValue("cid", customer.CustomerId);
                cmd.Parameters.AddWithValue("prid", product.ProductId);
                cmd.Parameters.AddWithValue("quan", quantity);
                int count = cmd.ExecuteNonQuery();
                con.Close();
                return count > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool removeFromCart(Customer customer, Product product)
        {
            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("delete from cart where customer_id=@cid and product_id=@pid", con);
                cmd.Parameters.AddWithValue("cid", customer.CustomerId);
                cmd.Parameters.AddWithValue("pid", product.ProductId);
                int count = cmd.ExecuteNonQuery();
                con.Close();
                return count > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public List<Product> getAllFromCart(Customer customer)
        {
            List<Product> products = new List<Product>();
            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                cmd = new SqlCommand("select p.product_id,p.name,p.price,p.description,p.stockQuantity,c.quantity from cart c join products p on c.product_id=p.product_id where c.customer_id=@cid", con);
                cmd.Parameters.AddWithValue("cid", customer.CustomerId);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product p = new Product{
                        ProductId = Convert.ToInt32(dr["product_id"]),
                        Name = dr["name"].ToString(),
                        Price = Convert.ToDecimal(dr["price"]),
                        Description = dr["description"].ToString(),
                        StockQuantity = Convert.ToInt32(dr["stockQuantity"])
                    };
                    products.Add(p);
                }
                con.Close();
                if(products.Count== 0) 
                throw new CustomerNotFoundException($"No cart found for customer ID {customer.CustomerId}.");

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
        public bool placeOrder(Customer customer, Dictionary<Product, int> items, string shippingAddress)
        {
            try
            {
                using (con = DBConnection.GetConnection())
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        decimal total = 0;
                        foreach (var item in items)
                        {
                            total += item.Key.Price * item.Value;
                        }
                        string insertOrder = "insert into orders (customer_id, order_date, total_price, shipping_address) OUTPUT INSERTED.order_id values (@custId, GETDATE(), @total, @address)";
                        cmd = new SqlCommand(insertOrder, con, transaction);
                        cmd.Parameters.AddWithValue("@custId", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@address", shippingAddress);
                        int orderId = (int)cmd.ExecuteScalar();
                        foreach (var item in items)
                        {
                            string insertItem = "insert into order_items (order_id, product_id, quantity) values (@orderId, @prodId, @qty)";
                            cmd = new SqlCommand(insertItem, con, transaction);
                            cmd.Parameters.AddWithValue("@orderId", orderId);
                            cmd.Parameters.AddWithValue("@prodId", item.Key.ProductId);
                            cmd.Parameters.AddWithValue("@qty", item.Value);
                            cmd.ExecuteNonQuery();

                            string updateStock = "update products set stockQuantity = stockQuantity - @orderQty where product_id = @prodId";
                            SqlCommand cmdUpdateStock = new SqlCommand(updateStock, con, transaction);
                            cmdUpdateStock.Parameters.AddWithValue("@orderQty", item.Value);
                            cmdUpdateStock.Parameters.AddWithValue("@prodId", item.Key.ProductId);
                            cmdUpdateStock.ExecuteNonQuery();

                            string checkCartQuantity = "select quantity from cart where customer_id = @custId AND product_id = @prodId";
                            SqlCommand cmdCheck = new SqlCommand(checkCartQuantity, con, transaction);
                            cmdCheck.Parameters.AddWithValue("@custId", customer.CustomerId);
                            cmdCheck.Parameters.AddWithValue("@prodId", item.Key.ProductId);
                            int currentCartQuantity = (int)cmdCheck.ExecuteScalar();

                            int remainingQuantity = currentCartQuantity - item.Value;

                            if (remainingQuantity > 0)
                            {
                                string updateCart = "update cart set quantity = @remainingQty where customer_id = @custId AND product_id = @prodId";
                                SqlCommand cmdUpdateCart = new SqlCommand(updateCart, con, transaction);
                                cmdUpdateCart.Parameters.AddWithValue("@remainingQty", remainingQuantity);
                                cmdUpdateCart.Parameters.AddWithValue("@custId", customer.CustomerId);
                                cmdUpdateCart.Parameters.AddWithValue("@prodId", item.Key.ProductId);
                                cmdUpdateCart.ExecuteNonQuery();
                            }
                            else
                            {
                                string removeItemFromCart = "delete from cart where customer_id = @custId AND product_id = @prodId";
                                SqlCommand cmdRemoveCart = new SqlCommand(removeItemFromCart, con, transaction);
                                cmdRemoveCart.Parameters.AddWithValue("@custId", customer.CustomerId);
                                cmdRemoveCart.Parameters.AddWithValue("@prodId", item.Key.ProductId);
                                cmdRemoveCart.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        con.Close();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in PlaceOrder: " + ex.Message);
                return false;
            }
        }

        public Dictionary<Product, int> getOrdersByCustomer(int customerId)
        {
            Dictionary<Product, int> orders = new Dictionary<Product, int>();

            try
            {
                con = DBConnection.GetConnection();
                con.Open();
                string query = "select p.product_id, p.name, p.price, p.description, p.stockQuantity, oi.quantity from orders o JOIN order_items oi ON o.order_id = oi.order_id JOIN products p ON oi.product_id = p.product_id where o.customer_id = @cid";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cid", customerId);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product p = new Product
                    {
                        ProductId = Convert.ToInt32(dr["product_id"]),
                        Name = dr["name"].ToString(),
                        Price = Convert.ToDecimal(dr["price"]),
                        Description = dr["description"].ToString(),
                        StockQuantity = Convert.ToInt32(dr["stockQuantity"])
                    };
           
                    int quantity = Convert.ToInt32(dr["quantity"]);
                    orders.Add(p, quantity);
                }

                if (orders.Count == 0)
                    throw new OrderNotFoundException($"No orders found for customer ID {customerId}.");
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            con.Close();

            return orders;
        }

        public List<Product> getAllProducts()
        {
            List<Product> list = new List<Product>();
            string query = "select * FROM products";
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand(query, con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product p = new Product
                    {
                        ProductId = (int)dr["product_id"],
                        Name = dr["name"].ToString(),
                        Price = (decimal)dr["price"],
                        Description = dr["description"].ToString(),
                        StockQuantity = (int)dr["stockQuantity"]
                    };
                    list.Add(p);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }

        public bool updateCustomer(Customer customer)
        {
            try
            {
                con = DBConnection.GetConnection();
                string query = "update customers set name = @name, email = @email, password = @password where customer_id = @id";

                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Parameters.AddWithValue("@id", customer.CustomerId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }


    }
}
