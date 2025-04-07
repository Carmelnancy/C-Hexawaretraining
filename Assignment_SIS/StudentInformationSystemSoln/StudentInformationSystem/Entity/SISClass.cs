using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.Entity
{
    public class SISClass
    {
        public List<Students> students { get; set; }
        public List<Courses> courses { get; set; }
        public List<Enrollments> enrollments { get; set; }
        public List<Teacher> teachers { get; set; }
        public List<Payments> payments { get; set; }

        public SISClass()
        {
            students= new List<Students>();
            courses= new List<Courses>();
            enrollments= new List<Enrollments>();
            teachers= new List<Teacher>();
            payments= new List<Payments>();
        }

        public void AddStudent(Students student)
        {
            students.Add(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public void AddCourse(Courses course)
        {
            courses.Add(course);
        }

        public void EnrollStudent(Enrollments enrollment)
        {
            enrollments.Add(enrollment);
        }

        public void MakePayment(Payments payment)
        {
            payments.Add(payment);
        }

        public void AddEnrollment(Students s,Courses c,DateOnly d)
        {
            foreach (var emp in enrollments)
            {
                if (emp.studentId == s.studentId && emp.courseId == c.courseId)
                {
                    throw new DuplicateEnrollmentException();
                }
            }
            Enrollments e= new Enrollments(enrollments.Count+1,s,c,d);

            enrollments.Add(e);
            s.Enrollments.Add(e);
            c.enrollments.Add(e);
            Console.WriteLine("Enrolled successfully");
        }

        public void AssignCourseToTeacher(Courses c,Teacher t)
        {
            if (t == null)
            {
                throw new TeacherNotFoundException();
            }
            t.AssignedCourses.Add(c);
            c.instructorName = t.firstName;

            Console.WriteLine("The teacher is assigned to the course successfully");
        }

        public void AddPayments(Students s,decimal a, DateOnly d)
        {
            if (a <= 0)
            {
                throw new PaymentValidationException();
            }
            Payments p = new Payments(payments.Count + 1, s, a, d);
            payments.Add(p);
            s.PaymentHistory.Add(p);

            Console.WriteLine("Payment added successfully");
        }

        public void GetEnrollmentsForStudent(Students s)
        {
            bool b= false;
            foreach (var e in enrollments)
            {
                if(e.studentId == s.studentId)
                {
                    Console.WriteLine($"{e.courseId}-{e.enrollmentDate}");
                    b= true;
                }
            }
            if (!b)
            {
                Console.WriteLine("The student has done no enrollments");
            }
        }

        public void GetCoursesForTeacher(Teacher t)
        {
            Console.WriteLine($"Courses assigned to the teacher {t.firstName} :");
            foreach(var c in t.AssignedCourses)
            {
                Console.WriteLine($"Course {c.courseName}");
            }
        }

        internal void GenerateEnrollmentReport(Courses course)
        {
            foreach(var e in course.enrollments)
            {
                Console.WriteLine($"Student ID: {e.Student.studentId}, Name: {e.Student.firstName} {e.Student.lastName}, Enrolled On: {e.enrollmentDate}");
            }
        }

        internal void GeneratePaymentReport(Students student)
        {
            foreach (var payment in student.PaymentHistory)
            {
                Console.WriteLine($"Amount: {payment.amount}, Date: {payment.paymentDate}");
            }
        }

        internal void CalculateCourseStatistics(Courses course)
        {
            int enrollmentCount = course.enrollments.Count;
            decimal totalPayments = 0;

            foreach (var enrollment in course.enrollments)
            {
                foreach (var payment in enrollment.Student.PaymentHistory)
                {
                    totalPayments += payment.amount;
                }
            }
            Console.WriteLine($"Course Statistics for: {course.courseName}");
            Console.WriteLine($"Total Enrollments: {enrollmentCount}");
            Console.WriteLine($"Total Payments : {totalPayments}");
        }



        public Courses GetCourseById(int courseId)
        {
            foreach (Courses course in courses)
            {
                if (course.courseId == courseId)
                    return course;
            }
            throw new CourseNotFoundException();
        }
       

        public  Students GetStudentById(int studentId)
        {
            foreach (Students student in students)
            {
                if (student.studentId == studentId)
                    return student;
            }
            throw new StudentNotFoundException();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            foreach (Teacher teacher in teachers)
            {
                if (teacher.teacherId == teacherId)
                    return teacher;
            }
            throw new TeacherNotFoundException();
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            foreach (Enrollments e in enrollments)
            {
                if (e.studentId == studentId && e.courseId == courseId)
                    throw new DuplicateEnrollmentException();
            }
        }

        public void UpdateStudentInfo(string name, string email, DateOnly dob)
        {
            if (dob > DateOnly.FromDateTime(DateTime.Now) || !email.Contains("@"))
                throw new InvalidStudentDataException();
        }

        public void UpdateCourseInfo(string courseCode, string courseName)
        {
            if (string.IsNullOrEmpty(courseCode) || string.IsNullOrEmpty(courseName))
                throw new InvalidCourseDataException();
        }

        public void UpdateTeacherInfo(string name, string email)
        {
            if (string.IsNullOrEmpty(name) || !email.Contains("@"))
                throw new InvalidTeacherDataException();


        }

        public void ValidateEnrollment(int studentId, int courseId)
        {
            bool studentFound = false, courseFound = false;

            foreach (var s in students)
                if (s.studentId == studentId)
                    studentFound = true;

            foreach (var c in courses)
                if (c.courseId == courseId)
                    courseFound = true;

            if (!studentFound || !courseFound)
                throw new InvalidEnrollmentDataException();
        }

        public void ValidatePayment(decimal amount, DateTime date)
        {
            if (amount <= 0 || date > DateTime.Now)
                throw new PaymentValidationException();
        }

        public void MakePayment(int student, decimal amount)
        {
            Students s  = GetStudentById(student);
            if (s.balance < amount)
                throw new InsufficientFundsException();

            s.balance -= amount;
        }
    }
}
