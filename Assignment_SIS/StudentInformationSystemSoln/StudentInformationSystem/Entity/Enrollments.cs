using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Enrollments
    {
        public int enrollmentId { get; set; }
        public int studentId { get; set; }
        public int courseId { get; set; }
        public DateOnly enrollmentDate { get; set; }
        public Students Student { get; set; }
        public Courses Course { get; set; }

        public Enrollments() { }
        public Enrollments(int enrollmentId, int studentId, int courseId, DateOnly enrollmentDate)
        {
            this.enrollmentId = enrollmentId;
            this.studentId = studentId;
            this.courseId = courseId;
            this.enrollmentDate = enrollmentDate;
        }
        public string courseName;
        public Enrollments(int enrollmentId, Students student, Courses course, DateOnly enrollmentDate)
        {
            this.enrollmentId = enrollmentId;
            Student = student;
            studentId = student.studentId;
            Course = course;
            courseId = (int)course.courseId;
            courseName=course.courseName;
            this.enrollmentDate = enrollmentDate;
        }

        public Enrollments(int sid, int couid, DateOnly edate)  // for db
        {
            this.studentId = sid;
            this.courseId = couid;
            this.enrollmentDate = edate;
        }

        //public int GetStudentId() 
        //{
        //    return studentId;
        //}
        public Students GetStudent()
        {
            return Student;
        }
        //public int GetCourse()
        //{
        //    return courseId;
        //}
        public Courses GetCourse()
        {
            return Course;
        }
    }
}
