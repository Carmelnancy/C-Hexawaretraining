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
    public class EnrollmentDao
    {

        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void GetAllEnrollments()
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("select * from Enrollments", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("Enrollment ID : " + dr[0] + "Student Id : " + dr[1] + "Course Id : " + dr[2] + "Enrollment date : " + dr[3] );
                }
                //dr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retrieving enrollments: " + ex.Message);
            }
        }
        public static void AddEnrollment(Enrollments e)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("insert into Enrollments values ( @stuid, @cid, @edate)", con);
                //dr = cmd.ExecuteReader();
               
                cmd.Parameters.AddWithValue("stuid", e.studentId);
                cmd.Parameters.AddWithValue("cid", e.courseId);
                cmd.Parameters.AddWithValue("edate",e.enrollmentDate.ToDateTime(TimeOnly.MinValue));
        
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {

                    Console.WriteLine("Enrollment added successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error adding enrollment: " + ex.Message);
            }
        }
        public static void UpdateEnrollment(Enrollments e)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("update Enrollments set course_id=@co,student_id=@sid,enrollment_date=@ed where enrollment_id=@id", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("co", e.courseId);
                cmd.Parameters.AddWithValue("sid", e.studentId);
                cmd.Parameters.AddWithValue("ed", e.enrollmentDate.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("id", e.enrollmentId);


                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Enrollment updated successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating enrollment: " + ex.Message);
            }
        }
    }
}
