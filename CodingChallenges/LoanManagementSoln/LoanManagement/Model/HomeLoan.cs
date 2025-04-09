using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Model
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue {  get; set; }

        public HomeLoan() : base () { }

        public HomeLoan(int loanId, Customer customer, double principalAmount, float interestRate,int loanTerm,string loanType,string loanStatus, string propertyAddress, int propertyValue) : base(loanId, customer, principalAmount, interestRate, loanTerm, loanType,loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"Property Address : {PropertyAddress} ,PropertyValue : {PropertyValue}");
        }
    }
}
