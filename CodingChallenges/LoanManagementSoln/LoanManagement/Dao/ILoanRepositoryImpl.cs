using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.Exceptions;
using System.Data.Sql.Client;
using LoanManagement.Model;
using System.Data.SqlClient;
using System.Data.Sql;
using LoanManagement.Util;
using System.Reflection.PortableExecutable;
using System.Data.SqlClient;



namespace LoanManagement.Dao
{
    public class ILoanRepositoryImpl : ILoanRepository
    {
        static SqlConnection con=null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Confirm to apply for laon (Y/N): ");
            var input =Console.ReadLine();
            if(input == "N")
            {
                return;
            }
            try
            {
                con = DBUtil.GetConnection();
                cmd = new SqlCommand("insert into Loan values (@loanId,@cid,@pAmount,@irate,@loanterm,@loantype,@loanstatus)", con);

                cmd.Parameters.AddWithValue("loanId", loan.LoanId);
                cmd.Parameters.AddWithValue("cid", loan.Customer.CustomerId);
                cmd.Parameters.AddWithValue("pAmount", loan.PrincipalAmount);
                cmd.Parameters.AddWithValue("irate", loan.InterestRate);
                cmd.Parameters.AddWithValue("loanterm", loan.LoanTerm);
                cmd.Parameters.AddWithValue("loantype", loan.LoanType);
                cmd.Parameters.AddWithValue("loanstatus", loan.LoanStatus);

                cmd.ExecuteNonQuery();

                if (loan is HomeLoan home)
                {
                    var homeCmd = new SqlCommand("insert into HomeLoan values (@LoanId, @PropertyAddress, @PropertyValue)", con);
                    homeCmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                    homeCmd.Parameters.AddWithValue("@PropertyAddress", home.PropertyAddress);
                    homeCmd.Parameters.AddWithValue("@PropertyValue", home.PropertyValue);
                    homeCmd.ExecuteNonQuery();
                }
                else if (loan is CarLoan car)
                {
                    var carCmd = new SqlCommand("insert into CarLoan values (@LoanId, @CarModel, @CarValue)", con);
                    carCmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                    carCmd.Parameters.AddWithValue("@CarModel", car.CarModel);
                    carCmd.Parameters.AddWithValue("@CarValue", car.CarValue);
                    carCmd.ExecuteNonQuery();
                }
                Console.WriteLine("Loan applied successfully");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        }

        //public double CalculateEMI(int loanId)
        //{
        //    try
        //    {
        //        con = DBUtil.GetConnection();
        //        cmd = new SqlCommand("select PrincipalAmount, InterestRate, LoanTerm FROM Loan where LoanId = @LoanId", con);
        //        cmd.Parameters.AddWithValue("LoanId", loanId);
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            double principal = dr.GetDouble(0);
        //            float rate = (float)dr.GetDouble(1);
        //            int term = dr.GetInt32(2);
        //            return CalculateEMI(principal, rate, term);
        //        }
        //        else
        //        {
        //            throw new InvalidLoanException($"Loan with ID {loanId} not found.");
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        public void CalculateEMI(int loanId)
        {
            try
            {
                con = DBUtil.GetConnection();
                cmd = new SqlCommand("SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanId = @LoanId", con);
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    double principal = dr.GetDouble(0);
                    float rate = (float)dr.GetDouble(1);
                    int term = dr.GetInt32(2);

                    double emi = CalculateEMI(principal, rate, term);
                    Console.WriteLine($"\nEMI for Loan ID {loanId}: ₹{emi:F2}");
                }
                else
                {
                    Console.WriteLine($"Loan with ID {loanId} not found.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
        }

        //public double CalculateEMI(double prncipal, float interestRate, int loanTerm)
        //{
        //    double r = interestRate / 1200;
        //    double emi=(prncipal*r*Math.Pow(1+r,loanTerm))/(Math.Pow(1+r,loanTerm)-1);
        //    return emi;
        //}
        public double CalculateEMI(double principal, float interestRate, int loanTerm)
        {
            double r = interestRate / 1200;
            double emi = (principal * r * Math.Pow(1 + r, loanTerm)) / (Math.Pow(1 + r, loanTerm) - 1);
            return emi;
        }

        //public double CalculateInterest(int loanId)
        //{
        //    try
        //    {
        //        con = DBUtil.GetConnection();
        //        cmd = new SqlCommand("select PrincipalAmount,InterestRate,LoanTerm from Loan where LoanId =@loanid", con);
        //        cmd.Parameters.AddWithValue("laonid", loanId);

        //        dr=cmd.ExecuteReader();

        //        if (dr.Read())
        //        {
        //            double principal=dr.GetDouble(0);
        //            float rate=(float)dr.GetDouble(1);
        //            int term = dr.GetInt32(2);
        //            return CalculateInterest(principal, rate, term);
        //        }
        //        else
        //        {
        //            throw new InvalidLoanException();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //}

        public double CalculateInterest(double principalAmount, float interestRate, int loanTerm)
        {
            return (principalAmount *interestRate*loanTerm) / 12;
        }

        //public List<Loan> GetAllLoan()
        //{
        //    List<Loan> loans = new List<Loan>();
        //    try
        //    {
        //        con = DBUtil.GetConnection();
        //        cmd = new SqlCommand("select LoanId,CustomerId,PrincipalAmount,InterestRate,LoanTerm,LoanType,LoanStatus from Loan", con);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            Loan loan = new Loan
        //            {
        //                LoanId = dr.GetInt32(0),
        //                Customer = new Customer { CustomerId = dr.GetInt32(1) },
        //                PrincipalAmount = dr.GetDouble(2),
        //                InterestRate = (float)dr.GetDouble(3),
        //                LoanTerm = dr.GetInt32(4),
        //                LoanType = dr.GetString(5),
        //                LoanStatus = dr.GetString(6)
        //            };
        //            loans.Add(loan);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        public void GetAllLoans()
        {
            try
            {
                con = DBUtil.GetConnection();
                cmd = new SqlCommand("SELECT LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus FROM Loan", con);
                dr = cmd.ExecuteReader();


                Console.WriteLine("LoanId | CustomerId | Principal | Interest | Term | Type | Status");
                while (dr.Read())
                {
                    int loanId = dr.GetInt32(0);
                    int customerId = dr.GetInt32(1);
                    double principal = dr.GetDouble(2);
                    float interest = (float)dr.GetDouble(3);
                    int term = dr.GetInt32(4);
                    string type = dr.GetString(5);
                    string status = dr.GetString(6);

                    Console.WriteLine($"{loanId} | {customerId} | {principal:C} | {interest}% | {term} months | {type} | {status}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error fetching loan data: " + ex.Message);
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();
                if (con != null)
                    con.Close();
            }
        }

        //public Loan GetLoanById(int loanId)
        //        {
        //            List<Loan> loans = new List<Loan>();
        //            try
        //            {
        //                con = DBUtil.GetConnection();
        //                cmd = new SqlCommand("select LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus FROM Loan WHERE LoanId = @LoanId", con);
        //                cmd.Parameters.AddWithValue("LoanId", loanId);

        //                dr = cmd.ExecuteReader();

        //                if (dr.Read())
        //                {
        //                    Loan loan=new Loan
        //                    {
        //                        LoanId = Convert.ToInt32(dr["LoanId"]),
        //                        Customer = new Customer { CustomerId = Convert.ToInt32(dr["CustomerId"]) },
        //                        PrincipalAmount = Convert.ToDouble(dr["PrincipalAmount"]),
        //                        InterestRate = Convert.ToSingle(dr["InterestRate"]),
        //                        LoanTerm = Convert.ToInt32(dr["LoanTerm"]),
        //                        LoanType = dr["LoanType"].ToString(),
        //                        LoanStatus = dr["LoanStatus"].ToString()
        //                    };
        //                    loans.Add(loan);
        //                }
        //                else
        //                {
        //                    throw new InvalidLoanException("Loan ID not found.");
        //                }

        //            }
        //            catch (SqlException ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        public void GetLoanById(int loanId)
        {
            try
            {
                con = DBUtil.GetConnection();
                cmd = new SqlCommand("SELECT LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus FROM Loan WHERE LoanId = @LoanId", con);
                cmd.Parameters.AddWithValue("@LoanId", loanId);

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Console.WriteLine("\n--- Loan Details ---");
                    Console.WriteLine("Loan ID: " + dr["LoanId"]);
                    Console.WriteLine("Customer ID: " + dr["CustomerId"]);
                    Console.WriteLine("Principal Amount: " + dr["PrincipalAmount"]);
                    Console.WriteLine("Interest Rate: " + dr["InterestRate"]);
                    Console.WriteLine("Loan Term: " + dr["LoanTerm"]);
                    Console.WriteLine("Loan Type: " + dr["LoanType"]);
                    Console.WriteLine("Loan Status: " + dr["LoanStatus"]);
                }
                else
                {
                    Console.WriteLine("Loan ID not found.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
        }
        //public void LoanRepayment(int loanId, double amount)
        //{
        //    con=DBUtil.GetConnection();
        //    double emi=CalculateEMI(loanId);
        //    if (amount < emi)
        //    {
        //        Console.WriteLine("Amount is less than one emi. Payment is rejected.");
        //        return;
        //    }

        //    int emiPaid = (int)(amount / emi);
        //    Console.WriteLine($"Payment accepted{emiPaid}");
        //}

        public void LoanStatus(int loanId)
        {
            try
            {
                con = DBUtil.GetConnection();
                cmd = new SqlCommand("select c.CreditScore from Customer c join Loan l on c.CustomerId=l.CustomerId where l.LoanId=@id", con);
                cmd.Parameters.AddWithValue("id", loanId);
                int creditScore = Convert.ToInt32("id", loanId);
                string status = creditScore >= 650 ? "Approved" : "Rejected";

                var cmd2 = new SqlCommand("Update Loan set LaonStatus=@status where LoanId=@id", con);
                cmd2.Parameters.AddWithValue("status", status);
                cmd2.Parameters.AddWithValue("id", loanId);
                cmd2.ExecuteNonQuery();

                Console.WriteLine($"Status of loan id {loanId} is {status}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message );
            }
        }

        double ILoanRepository.CalculateEMI(int loanId)
        {
            throw new NotImplementedException();
        }

        double ILoanRepository.CalculateInterest(int loanId)
        {
            throw new NotImplementedException();
        }

        void ILoanRepository.LoanRepayment(int loanId, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
