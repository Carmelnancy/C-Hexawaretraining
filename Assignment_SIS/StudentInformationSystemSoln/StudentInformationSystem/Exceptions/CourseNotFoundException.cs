using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class CourseNotFoundException : ApplicationException
    {
        public CourseNotFoundException() : base("The specified course does not exist.") { }
        public CourseNotFoundException(string message) : base(message) { }
    }
}
