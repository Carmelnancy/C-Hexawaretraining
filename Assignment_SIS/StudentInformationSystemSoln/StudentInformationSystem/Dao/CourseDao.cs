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
    public class CourseDao
    {

        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void GetAllCourses()
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("select * from Courses", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("Course ID : " + dr[0] + "Course Name : " + dr[1] + "Credits : " + dr[2] + "Teacher Id" + dr[3] );
                }
                //dr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retrieving Courses: " + ex.Message);
            }
        }
        public static void InsertCourse(Courses c)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("insert into Courses values (@Cn,@credts,@tid)", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("Cn", c.courseName);
                cmd.Parameters.AddWithValue("credits", c.credits);
                cmd.Parameters.AddWithValue("tid", c.teacherId);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {

                    Console.WriteLine("Course added successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error adding course: " + ex.Message);
            }
        }
        public static void UpdateCourse(Courses c)
        {
            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand("update Courses set course_name=@e,credits =@f,teacher_id=@l where course_id=@id", con);
                //dr = cmd.ExecuteReader();

                cmd.Parameters.AddWithValue("e",c.courseName);
                cmd.Parameters.AddWithValue("f",c.credits);
                cmd.Parameters.AddWithValue("l", c.teacherId);
                cmd.Parameters.AddWithValue("id", c.courseId);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Course updated successfully");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating course: " + ex.Message);
            }
        }
    }
}
