using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class TeacherDao
    {

        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void GetAllTeachers()
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("select * from Teacher", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ID : " + dr[0] + "FirstName : " + dr[1] + "LastName : " + dr[2] + "Email : " + dr[3]);
                }
           
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retrieving teachers: " + ex.Message);
            }
        }

        public static void InsertTeacher(Teacher t)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("insert into Teacher values (@FirstName, @LastName, @Email)", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("FirstName",t.firstName);
                cmd.Parameters.AddWithValue("LastName", t.lastName);
                cmd.Parameters.AddWithValue("Email", t.email);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {

                    Console.WriteLine("Teacher added successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error adding teacher: " + ex.Message);
            }
        }

         public static void UpdateTeacher(Teacher t)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("update Teacher set first_name =@f,last_name=@l,emai=@e where teacher_id=@id", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("f", t.firstName);
                cmd.Parameters.AddWithValue("l", t.lastName);
                cmd.Parameters.AddWithValue("e", t.email);
                cmd.Parameters.AddWithValue("id", t.teacherId);
     

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Teacher updated successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating teacher: " + ex.Message);
            }
        }
    }
}
