using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public double PrincipalAmount { get; set; }
        public float InterestRate { get; set; }
        public int LoanTerm {  get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan() { }

        public Loan(int loanId, Customer customer, double principalAmount, float interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public virtual void PrintDetails()
        {
            Console.WriteLine($"Loan id : {LoanId} , LoanType : {LoanType} , Status : {LoanStatus} , Amount : {PrincipalAmount} , Interest Rate : {InterestRate} , Term : {LoanTerm}");
            Customer.PrintDetails();
        }
    }
}
