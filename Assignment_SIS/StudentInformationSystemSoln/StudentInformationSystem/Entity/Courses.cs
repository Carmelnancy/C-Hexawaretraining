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
        public Courses(int courseId, string courseName, string courseCode, string instructorName)
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
        public void UpdateCourseInfo(int cc, string cn, int tid)
        {
            if (cc==0 || string.IsNullOrEmpty(cn))
            { 
            throw new InvalidCourseDataException("Course code and instructor name must be provided.");
            }
            this.courseId= cc;
            this.courseName= cn;
            this.teacherId= tid;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {courseId}, Name: {courseName}, Credits: {credits}, Teacher Id: {teacherId}");
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
