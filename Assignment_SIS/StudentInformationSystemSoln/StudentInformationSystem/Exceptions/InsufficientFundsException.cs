using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException() : base("Student does not have enough funds to enroll in this course.") { }

        public InsufficientFundsException(string message) : base(message) { }
    }
}
