using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.Dao;
using LoanManagement.Model;

namespace LoanManagement.Main
{
    public class LoanManagementMain
    {
        public static void Main(string[] args)
        {
            ILoanRepository repo = new ILoanRepositoryImpl();

            while (true)
            {
                Console.WriteLine("Loan Management Menu");
                Console.WriteLine("1. Apply Loan");
                Console.WriteLine("2. View All Loans");
                Console.WriteLine("3. Get Loan By ID");
                Console.WriteLine("4. Calculate EMI");
                Console.WriteLine("5. Calculate Interest");
                Console.WriteLine("6. Loan Status Approval");
                Console.WriteLine("7. Repay Loan");
                Console.WriteLine("8. Exit");
                Console.Write("Enter choice: ");
                int ch = int.Parse(Console.ReadLine());

                try
                {
                    switch (ch)
                    {
                        case 1:
                            Customer c = new(101, "Alice", "alice@email.com", "9999999999", "Wonderland", 700);
                            HomeLoan homeLoan = new(1, c, 1000000, 8.5f, 120, "HomeLoan", "Pending", "Dream Home", 1200000);
                            repo.ApplyLoan(homeLoan);
                            break;
                        case 2:
                            repo.GetAllLoans();
                            break;

                        case 3:
                            Console.Write("Enter Loan ID: ");
                            int id = int.Parse(Console.ReadLine());
                            repo.GetLoanById(id);
                            //loan.PrintDetails();
                            break;
                        case 4:
                            Console.Write("Enter Loan ID: ");
                            int emiId = int.Parse(Console.ReadLine());
                           repo.CalculateEMI(emiId);
                            break;
                        //case 5:
                        //    Console.Write("Enter Loan ID: ");
                        //    int intId = int.Parse(Console.ReadLine());
                        //    Console.WriteLine("Interest: " + repo.CalculateInterest(intId));
                        //    break;
                        case 6:
                            Console.Write("Enter Loan ID: ");
                            int statusId = int.Parse(Console.ReadLine());
                            repo.LoanStatus(statusId);
                            break;
                        //case 7:
                        //    Console.Write("Enter Loan ID: ");
                        //    int repayId = int.Parse(Console.ReadLine());
                        //    Console.Write("Enter Amount: ");
                        //    double amt = double.Parse(Console.ReadLine());
                        //    repo.LoanRepayment(repayId, amt);
                        //    break;
                        case 8:
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}

