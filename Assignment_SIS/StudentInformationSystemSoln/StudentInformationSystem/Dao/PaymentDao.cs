using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Exceptions;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class PaymentDao
    {

        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void GetAllPayments()
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("select * from Payments", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("Payment ID : " + dr[0] + "Student ID : " + dr[1] + "Amount : " + dr[2] + "Payment Date" + dr[3]);
                }
                //dr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retrieving payments: " + ex.Message);
            }
        }
        public static void AddPayment(Payments p)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("insert into Payments values ( @stuid, @amount, @edate)", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("stuid", p.studentId);
                cmd.Parameters.AddWithValue("amount", p.amount);
                cmd.Parameters.AddWithValue("edate", p.paymentDate.ToDateTime(new TimeOnly(0, 0)));

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {

                    Console.WriteLine("Payment added successfully");
                }
                con.Close();

                con = DBConnection.GetConnection();
                cmd = new SqlCommand("update Students set Balance=Balance-@amount where student_id=@stuid", con);

                cmd.Parameters.AddWithValue("amount", p.amount);
                cmd.Parameters.AddWithValue("stuid",p.studentId);
                int count2 = cmd.ExecuteNonQuery(); 
                if (count2 > 0)
                {
                    Console.WriteLine("Updated balance in student table");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error adding payment: " + ex.Message);
            }
        }
    }
}
