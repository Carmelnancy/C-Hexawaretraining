using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InvalidStudentDataException : ApplicationException
    {
        public InvalidStudentDataException() : base("Invalid data provided for student creation or update.") { }
        public InvalidStudentDataException(string message) : base(message) { }
    }
}
