using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class DuplicateEnrollmentException : ApplicationException
    {
        public DuplicateEnrollmentException() : base("Student is already enrolled in this course.") { }
        public DuplicateEnrollmentException(string message) : base(message) { }
    }
}
