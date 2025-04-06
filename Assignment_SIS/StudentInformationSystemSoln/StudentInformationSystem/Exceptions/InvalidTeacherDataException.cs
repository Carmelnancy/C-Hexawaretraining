using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InvalidTeacherDataException : ApplicationException
    {
        public InvalidTeacherDataException() : base("Invalid data provided for teacher creation or update.") { }
        public InvalidTeacherDataException(string message) : base(message){ }
    }
}
