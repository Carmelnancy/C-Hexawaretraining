using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.Entity
{
    public class Courses
    {
        public int ?courseId { get; set; }
        public String courseName { get; set; }
        public String courseCode { get; set; }
        public String instructorName { get; set; }
        public int credits { get; set; }   // for SIS databse 
        public int teacherId { get; set; } // for SIS database
        public List<Enrollments> enrollments { get; set; } = new List<Enrollments>();


        public Courses()
        {
            
        }
        public Courses(int courseId, string courseCode, string courseName, string instructorName)
        {
            this.courseId = courseId;
            this.courseName = courseName;
            this.courseCode = courseCode;
            this.instructorName = instructorName;
        }

        public Courses(string courseName, int credits, int teacherId)  // constructor for database
        {
            this.courseName = courseName;
            this.credits = credits;
            this.teacherId = teacherId;

        }
        public Courses(int cid, string cName, int cred, int teaId) // for db
        {
            this.courseId = cid;
            this.courseName = cName;
            this.credits = cred;
            this.teacherId = teaId;
        }

  

        public void AssignTeacher(Teacher t)
        {
            teacherId= t.teacherId;
            t.AssignedCourses.Add(this);
        }
        public void UpdateCourseInfo(string cc, string cn, string ins)
        {
            if (string.IsNullOrEmpty(cc) || string.IsNullOrEmpty(cn))
                throw new InvalidCourseDataException();
            this.courseCode= cc;
            this.courseName= cn;
            this.instructorName= ins;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {courseId}, CourseCode: {courseCode}, Name: {courseName}, Teacher: {instructorName}");
        }

        public List<Enrollments> GetEnrollments()
        {
            return enrollments;
        }

        public int GetTeacher()
        {
            return teacherId;
        }


       
    }
}
