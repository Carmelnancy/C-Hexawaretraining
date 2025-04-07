using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.Entity
{
    public class Teacher
    {
        public int teacherId {  get; set; }
        public string firstName {  get; set; }
        public string lastName {  get; set; }
        public string email {  get; set; }
        public List<Courses> AssignedCourses { get; set; }

        public Teacher() { }
        public Teacher(int teacherId, string firstName, string lastName,string email)
        {
            this.teacherId = teacherId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            AssignedCourses = new List<Courses>();
        }

        public Teacher(string fn, string ln, string email)  // for db
        {
            this.firstName=fn;
            this.lastName=ln;
            this.email = email;
        }
        public void UpdateTeacherInfo(string fn, string ln, string email)
        {
            if (string.IsNullOrEmpty(fn) || !email.Contains("@"))
            {
                throw new InvalidTeacherDataException("Invalid name or email format.");
            }
            this.firstName= fn;
            this.lastName= ln;
            this.email= email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher ID: {teacherId}, Name: {firstName} {lastName}, Email: {email}");
        }

        public List<Courses> GetAssignedCourses()
        {
            return AssignedCourses;
        }
    }
}
