using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    internal class StudentDao
    {
        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void GetAllStudents()
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("select * from Students",con);
                dr= cmd.ExecuteReader();
            
                while (dr.Read())
                {
                    Console.WriteLine("ID : "+ dr[0] +"FirstName : "+ dr[1]+"LastName : " + dr[2]+"DOB : " + dr[3]+"Email : " + dr[4]+"Phone num : " + dr[5]);
                }
                //dr.Close();
                con.Close();
                
            }
            catch (SqlException ex) 
            {
                Console.WriteLine("Error retrieving students: " + ex.Message);
            }
        }
        public static void InsertStudents(Students s)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("insert into Students values (@FirstName, @LastName, @DOB, @Email, @Phone)", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("FirstName",s.firstName);
                cmd.Parameters.AddWithValue("LastName",s.lastName );
                cmd.Parameters.AddWithValue("DOB", s.dateOfBirth.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("Email",s.email);
                cmd.Parameters.AddWithValue("Phone", s.phone);
                //cmd.Parameters.AddWithValue("did", deptid);

                int count=cmd.ExecuteNonQuery();
                if (count > 0)
                {

                    Console.WriteLine("Student added successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error adding students: " + ex.Message);
            }
        }
        public static void UpdateStudents(Students s)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("update Students set email=@e,first_name =@f,last_name=@l,date_of_birth=@b,phone_number=@p where student_id=@id", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("f", s.firstName);
                cmd.Parameters.AddWithValue("l", s.lastName);
                cmd.Parameters.AddWithValue("b", s.dateOfBirth.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("e", s.email);
                cmd.Parameters.AddWithValue("id", s.studentId);
                cmd.Parameters.AddWithValue("p", s.phone);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Student updated successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating students: " + ex.Message);
            }
        }
    }
}
