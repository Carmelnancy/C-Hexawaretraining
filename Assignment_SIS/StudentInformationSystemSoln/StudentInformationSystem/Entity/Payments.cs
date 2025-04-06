using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Payments
    {
        public int paymentId {  get; set; }
        public int studentId {  get; set; }
        public Students Student { get; set; }
        public decimal amount {  get; set; }
        public DateOnly paymentDate {  get; set; }


        public Payments() { }
        public Payments(int paymentId, int studentId, decimal amount, DateOnly paymentDate)
        {
            this.paymentId = paymentId;
            this.studentId = studentId;
            this.amount = amount;
            this.paymentDate = paymentDate;
        }

        public Payments(int paymentId, Students student, decimal amount, DateOnly paymentDate)
        {
            this.paymentId = paymentId;
            Student = student;
            studentId = student.studentId;
            this.amount = amount;
            this.paymentDate = paymentDate;
        }

        public Payments(int sid, decimal price, DateOnly d)  // for db
        {
            this.studentId = sid;
            this.amount = price;
            this.paymentDate = d;
        }

        public int GetStudent()
        {
            return studentId;
        }
        //public Students GetStudent()
        //{
        //    return Student;
        //}

        public decimal GetPaymentAmount()
        {
            return amount;
        }

        public DateOnly GetPaymentDate()
        {
            return paymentDate;
        }
    }
}
