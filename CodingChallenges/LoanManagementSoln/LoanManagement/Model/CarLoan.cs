using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Model
{
    public class CarLoan : Loan
    {
        public string CarModel {  get; set; }
        public int CarValue { get; set; }

        public CarLoan() : base() { }

        public CarLoan(int loanId, Customer customer, double principalAmount, float interestRate, int loanTerm, string loanType, string loanStatus,string carModel,int carValue) : base(loanId, customer, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"Car Model : {CarModel} , Car Value : {CarValue}");
        }
    }
}
