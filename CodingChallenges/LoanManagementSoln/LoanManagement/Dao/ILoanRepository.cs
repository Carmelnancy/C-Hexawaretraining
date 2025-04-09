using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.Model;

namespace LoanManagement.Dao
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);

        double CalculateInterest(int loanId);
        double CalculateInterest(double principalAmount,float interestRate,int loanTerm);

        void LoanStatus(int loanId);

        double CalculateEMI(int loanId);
        double CalculateEMI(double prncipalAmount,float interestRate,int loanTerm);

        void LoanRepayment(int loanId, double amount);

        void GetAllLoans();

        void GetLoanById(int loanId);

    }
}
