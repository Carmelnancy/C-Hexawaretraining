using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InvalidEnrollmentDataException : ApplicationException
    {
        public InvalidEnrollmentDataException() : base("Invalid data provided for enrollment.") { }
        public InvalidEnrollmentDataException(string message) : base(message){ }
    }
}
