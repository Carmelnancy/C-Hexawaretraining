using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Model
{
    public class Customer
    {
        public int CustomerId {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        public Customer() { }

        public Customer(int customerId, string name, string email, string phoneNumber, string address, int creditScore)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditScore = creditScore;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Customer Id : {CustomerId} ,Name : {Name} ,Email : {Email} ,Phone : {PhoneNumber} ,Address : {Address} ,CreditScore : {CreditScore}");
        }
    }
}
