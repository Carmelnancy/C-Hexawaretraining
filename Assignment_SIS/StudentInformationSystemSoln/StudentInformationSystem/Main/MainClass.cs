using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Dao;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.Main
{
    public class MainClass
    {
        static SISClass sis = new SISClass();
        public static void Main(string[] args)
        {

            Task3_4_5();

            // TASK 7 + TASK 8 + TASK 9 + TASK 10 + TASK 11

            while (true)
            {
                Console.WriteLine("Student Infromation System\n");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Enrollment Management");
                Console.WriteLine("4. Payment Management");
                Console.WriteLine("5. Teacher Management");
                Console.WriteLine("6. Make dynamic query");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");
                int c = int.Parse(Console.ReadLine());

                switch (c)
                {
                    case 1:
                        StudentMenu();
                        break;
                    case 2:
                        CourseMenu();
                        break;
                    case 3:
                        EnrollmentMenu();
                        break;
                    case 4:
                        PaymentMenu();
                        break;
                    case 5:
                        TeacherMenu();
                        break;
                    case 6:
                        DynamicQueryMenu();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            Console.ReadKey();
        }

        static void StudentMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Student Menu ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. List All Students");
                //Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:

                        Console.Write("Enter first Name: ");
                        string fn = Console.ReadLine();
                        Console.Write("Enter last Name: ");
                        string ln = Console.ReadLine();
                        Console.Write("Enter date of birth: ");
                        DateOnly db = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone: ");
                        string phone = Console.ReadLine();
                        Console.WriteLine("Enter the balance :");
                        decimal bal=Convert.ToDecimal(Console.ReadLine());
                        StudentDao.InsertStudents(new Students(fn, ln, db, email, phone,bal));
                        break;
                    case 2:

                        Console.WriteLine("Enter student ID to update:");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new first name:");
                        string nfn = Console.ReadLine();
                        Console.WriteLine("Enter new last name:");
                        string nln = Console.ReadLine();
                        Console.WriteLine("Enter new student date of birthl:");
                        DateOnly d= DateOnly.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new student email:");
                        string newEmail = Console.ReadLine();
                        Console.WriteLine("Enter new student phone number:");
                        string newPhone = Console.ReadLine();
                        Console.WriteLine("Enter the balance:");
                        decimal newBal= Convert.ToDecimal(Console.ReadLine());
     
                        StudentDao.UpdateStudents(new Students(id,nfn,nln,d,newEmail,newPhone,newBal));
                        break;
                    case 3:
                        StudentDao.GetAllStudents();
                        break;
                    //case 4:
                    //    StudentDao.DeleteStudent();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void TeacherMenu()
        {
            while (true)
            {
                Console.WriteLine("--- Teacher Menu ---");
                Console.WriteLine("1. Add Teacher");
                Console.WriteLine("2. Update Teacher");
                Console.WriteLine("3. List All Teachers");
                //Console.WriteLine("4. Delete Teacher");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:

                        Console.Write("Enter first Name: ");
                        string fn = Console.ReadLine();
                        Console.Write("Enter last Name: ");
                        string ln = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        TeacherDao.InsertTeacher(new Teacher(fn, ln, email));
                        break;
                    case 2:

                        Console.WriteLine("Enter teacher ID to update:");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new first name:");
                        string nfn = Console.ReadLine();
                        Console.WriteLine("Enter new last name:");
                        string nln = Console.ReadLine();
                        Console.WriteLine("Enter new email:");
                        string newEmail = Console.ReadLine();

                        TeacherDao.UpdateTeacher(new Teacher(id, nfn, nln, newEmail));
                        break;
                    case 3:
                        StudentDao.GetAllStudents();
                        break;
                        //case 4:
                        //    StudentDao.DeleteStudent();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void CourseMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Course Menu ---");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. List All Courses");
                //Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        Console.Write("Enter Course Name: ");
                        string courseName = Console.ReadLine();
                        Console.Write("Enter Credits: ");
                        int credits = int.Parse(Console.ReadLine());
                        Console.Write("Enter Teacher ID: ");
                        int teacherId = int.Parse(Console.ReadLine());
                        CourseDao.InsertCourse(new Courses(courseName, credits, teacherId));
                        break;
                    case 2:
                        Console.Write("Enter Course id to update: ");
                        int cid =int.Parse( Console.ReadLine());
                        Console.Write("Enter Course Name: ");
                        string cName = Console.ReadLine();
                        Console.Write("Enter Credits: ");
                        int cred = int.Parse(Console.ReadLine());
                        Console.Write("Enter Teacher ID: ");
                        int teaId = int.Parse(Console.ReadLine());
                        CourseDao.UpdateCourse(new Courses(cid,cName,cred,teaId));
                        break;
                    case 3:
                        CourseDao.GetAllCourses();
                        break;
                    //case 4:
                    //    CourseDao.DeleteCourse();
                    //    break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void EnrollmentMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Enrollment Menu ---");
                Console.WriteLine("1. Add Enrollment");
                Console.WriteLine("2. View All Enrollments");
                Console.WriteLine("3. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter the student id:");
                        int sid=int.Parse( Console.ReadLine());
                        Console.WriteLine("Enter the course id:");
                        int couid=int.Parse( Console.ReadLine());
                        Console.WriteLine("Enter the enrollment date:");
                        DateOnly edate=DateOnly.Parse( Console.ReadLine());
                        EnrollmentDao.AddEnrollment(new Enrollments(sid,couid,edate));
                        break;
                    case 2:
                        EnrollmentDao.GetAllEnrollments();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void PaymentMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Payment Menu ---");
                Console.WriteLine("1. Add Payment");
                Console.WriteLine("2. View All Payments");
                Console.WriteLine("3. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter the student id :");
                        int sid= int.Parse( Console.ReadLine());
                        Console.WriteLine("Enter the amount :");
                        decimal price=decimal.Parse( Console.ReadLine());
                        Console.WriteLine("Enter the payment date :");
                        DateOnly d=DateOnly.Parse(Console.ReadLine());
                        PaymentDao.AddPayment(new Payments(sid,price,d));
                        break;
                    case 2:
                        PaymentDao.GetAllPayments();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        static void DynamicQueryMenu()
        {
            DynamicQueryBuilder.MakeDynamicQuery();
        }

        public static void Task3_4_5()
        {
    
            Students s1 = new Students(1, "Nancy", "Sparks", new DateOnly(2003, 1, 28), "nancy@example.com", "9876543210", 5000);
            Students s2 = new Students(2, "Mary", "Vasanthy", new DateOnly(2002, 5, 15), "mary@example.com", "9123456789", 3000);
            Students s3 = new Students(3, "Mancy", "Joy", new DateOnly(2004, 7, 12), "mancy@example.com", "9988776655", 4500);
            Students s4 = new Students(4, "George", "Stephen", new DateOnly(2001, 3, 22), "george@example.com", "9012345678", 2500);
            Students s5 = new Students(5, "Leanna", "Joy", new DateOnly(2003, 11, 5), "leanna@example.com", "9871234560", 6000);

            sis.AddStudent(s1);
            sis.AddStudent(s2);
            sis.AddStudent(s3);
            sis.AddStudent(s4);
            sis.AddStudent(s5);

    
            Teacher t1 = new Teacher(1, "Alex", "Johnson", "alex@school.com");
            Teacher t2 = new Teacher(2, "Linda", "Green", "linda@school.com");
            Teacher t3 = new Teacher(3, "Robert", "King", "robert@school.com");
            Teacher t4 = new Teacher(4, "Sophia", "White", "sophia@school.com");
            Teacher t5 = new Teacher(5, "Mark", "Lee", "mark@school.com");

            sis.AddTeacher(t1);
            sis.AddTeacher(t2);
            sis.AddTeacher(t3);
            sis.AddTeacher(t4);
            sis.AddTeacher(t5);

     
            Courses c1 = new Courses(1, "M1", "Mathematics", "Alex Johnson");
            Courses c2 = new Courses(2, "S1", "Science", "Linda Green");
            Courses c3 = new Courses(3, "E1", "English", "Robert King");
            Courses c4 = new Courses(4, "H1", "History", "Sophia White");
            Courses c5 = new Courses(5, "C1", "Computer", "Mark Lee");

            sis.AddCourse(c1);
            sis.AddCourse(c2);
            sis.AddCourse(c3);
            sis.AddCourse(c4);
            sis.AddCourse(c5);

  
            Enrollments e1 = new Enrollments(1, s1, c1, new DateOnly(2024, 1, 10));
            Enrollments e2 = new Enrollments(2, s2, c2, new DateOnly(2024, 2, 5));
            Enrollments e3 = new Enrollments(3, s3, c3, new DateOnly(2024, 2, 10));
            Enrollments e4 = new Enrollments(4, s4, c1, new DateOnly(2024, 3, 1));
            Enrollments e5 = new Enrollments(5, s5, c4, new DateOnly(2024, 3, 15));

            sis.EnrollStudent(e1);
            sis.EnrollStudent(e2);
            sis.EnrollStudent(e3);
            sis.EnrollStudent(e4);
            sis.EnrollStudent(e5);

  
            Payments p1 = new Payments(1, s1, 5000, new DateOnly(2024, 1, 15));
            Payments p2 = new Payments(2, s2, 4500, new DateOnly(2024, 2, 10));
            Payments p3 = new Payments(3, s3, 4000, new DateOnly(2024, 2, 20));
            Payments p4 = new Payments(4, s4, 4800, new DateOnly(2024, 3, 5));
            Payments p5 = new Payments(5, s5, 5200, new DateOnly(2024, 3, 25));

            sis.MakePayment(p1);
            sis.MakePayment(p2);
            sis.MakePayment(p3);
            sis.MakePayment(p4);
            sis.MakePayment(p5);


            // --------------------------------------------- TASK 3  --------------------------------------------------


            s1.DisplayStudentInfo();

            s1.UpdateStudentInfo("Johnny", "D", new DateOnly(2001, 6, 15), "johnny.d@example.com", "9876543210", 5000);
            s1.DisplayStudentInfo();

            c1.DisplayCourseInfo();
            c1.UpdateCourseInfo("M1", "Mathematics", "Anne Johnson");

            c1.AssignTeacher(t1);

            t1.DisplayTeacherInfo();

            foreach (var c in t1.GetAssignedCourses())
            {
                c.DisplayCourseInfo();
            }

            s1.EnrollInCourse(c2);

            var enrolledCourses = s1.GetEnrolledCourses();
            foreach (var c in enrolledCourses)
            {
                c.DisplayCourseInfo();
            }

            var courseEnrollments = c2.GetEnrollments();
            foreach (var e in courseEnrollments)
            {
                Console.WriteLine($"Enrolled: {e.GetStudent().firstName} in {e.GetCourse().courseName} on {e.enrollmentDate}");
            }

            s1.MakePayment(2000, DateOnly.FromDateTime(DateTime.Now));

            var payments = s1.GetPaymentHistory();
            foreach (var p in payments)
            {
                Console.WriteLine($"Paid: {p.GetPaymentAmount()} on {p.GetPaymentDate().ToShortDateString()}");
            }

            sis.GenerateEnrollmentReport(c1);
            sis.GeneratePaymentReport(s1);
            sis.CalculateCourseStatistics(c2);


            //-----------------------------------------  TASK 4 ------------------------------------------------------- 

            try
            {
                // 1. CourseNotFoundException
                sis.GetCourseById(99);
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 2. StudentNotFoundException
                sis.GetStudentById(89);
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 3. TeacherNotFoundException
                sis.GetTeacherById(56);
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 4. DuplicateEnrollmentException
                sis.EnrollStudent(1, 1);
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 5. InvalidStudentDataException
                sis.UpdateStudentInfo("John", "invalid_email", new DateOnly(2025, 12, 25));
            }
            catch (InvalidStudentDataException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 6. InvalidCourseDataException
                sis.UpdateCourseInfo("M1", "");
            }
            catch (InvalidCourseDataException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 7. InvalidTeacherDataException
                sis.UpdateTeacherInfo("", "no_email");
            }
            catch (InvalidTeacherDataException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 8. InvalidEnrollmentDataException
                sis.ValidateEnrollment(0, 0);
            }
            catch (InvalidEnrollmentDataException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 9. PaymentValidationException
                sis.ValidatePayment(-500, DateTime.Now.AddDays(5));
            }
            catch (PaymentValidationException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }

            try
            {
                // 10. InsufficientFundsException
                sis.MakePayment(5, 99999);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }


            // ------------------------------------------- TASK 5 ---------------------------------------------------



            sis.AssignCourseToTeacher(c1, t1);
            sis.AssignCourseToTeacher(c2, t2);

            sis.AddEnrollment(s4, c5, DateOnly.FromDateTime(DateTime.Now));
            sis.AddEnrollment(s2, c1, DateOnly.FromDateTime(DateTime.Now));

            sis.AddPayments(s1, 2000, DateOnly.FromDateTime(DateTime.Now));

            sis.GetEnrollmentsForStudent(s1);
            sis.GetCoursesForTeacher(t1);

        }
    }
}
