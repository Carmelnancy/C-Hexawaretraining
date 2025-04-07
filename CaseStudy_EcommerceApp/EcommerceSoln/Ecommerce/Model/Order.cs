using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Order
    {
        private int orderId;
        private int customerId;
        private DateOnly date;
        private decimal totalPrice;
        private string shippingAddress;

        public Order() { }

        public Order(int orderId, int customerId, DateOnly date, decimal totalPrice, string shippingAddress)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.date = date;
            this.totalPrice = totalPrice;
            this.shippingAddress = shippingAddress;
        }

        public int OrderId { get => orderId; set => orderId = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public DateOnly Date { get => date; set => date = value; }
        public decimal TotalPrice { get => totalPrice; set => totalPrice = value; }
        public string ShippingAddress { get => shippingAddress; set => shippingAddress = value; }
    }
}
