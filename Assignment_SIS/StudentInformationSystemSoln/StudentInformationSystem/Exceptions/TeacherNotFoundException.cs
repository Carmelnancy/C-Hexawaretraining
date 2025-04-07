using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class TeacherNotFoundException : ApplicationException
    {
        public TeacherNotFoundException() : base("The specified teacher does not exist.") { }
        public TeacherNotFoundException(string message) : base(message) { }
    }
}
