using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Dao;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.Entity
{
    public class Students
    {
        public int studentId { get; set; }
        public String firstName { get;set; }
        public String lastName { get;set; }
        public DateOnly dateOfBirth { get; set; }
        public String email { get; set; }
        public String phone {  get; set; }
        public decimal balance { get; set; }  // additionally included for completing the task 10


        public List<Enrollments> Enrollments { get; set; }=new List<Enrollments>();
        public List<Payments> PaymentHistory { get; set; } = new List<Payments>();
        public Students() 
        {
        }
        public Students(int studentId, string firstName, string lastName, DateOnly dateOfBirth, string email, string phone,decimal balance)// db
        {
            this.studentId = studentId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.email = email;
            this.phone = phone;
            this.balance = balance;
        }

        public Students(string fn, string ln, DateOnly db, string email, string phone,decimal bal)  // for db
        {
            this.firstName = fn;
            this.lastName = ln;
            this.dateOfBirth = db;
            this.email= email;
            this.phone = phone;
            this.balance = bal;
        }


        public void EnrollInCourse(Courses c)
        {
            Enrollments e = new Enrollments
            {
                Student = this,
                Course = c,
                enrollmentDate = DateOnly.FromDateTime(DateTime.Now)
            };
            Enrollments.Add(e);
            c.enrollments.Add(e);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateOnly dateOfBirth, string email, string phoneNumber,decimal d)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now) || !email.Contains("@"))
            {
                throw new InvalidStudentDataException("Invalid DOB or email format.");
            }
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.email = email;
            this.phone = phoneNumber;
            this.balance = d;
        }

        public void MakePayment(decimal am, DateOnly pDate)
        {
            Payments payments = new Payments
            {
                Student = this,
                amount = am,
                paymentDate=pDate
            };
            PaymentHistory.Add(payments);
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student ID: {studentId}, Name: {firstName} {lastName}, DOB: {dateOfBirth.ToShortDateString()}, Email: {email}, Phone: {phone} ,Balance: {balance}");
        }

        public List<Courses> GetEnrolledCourses()
        {
            List<Courses> c= new List<Courses>();
            foreach (var e in Enrollments)
            {
                c.Add(e.Course);
            }
            return c;
        }
        public List<Payments> GetPaymentHistory()
        {
            return PaymentHistory;
        }

        
    }
}
