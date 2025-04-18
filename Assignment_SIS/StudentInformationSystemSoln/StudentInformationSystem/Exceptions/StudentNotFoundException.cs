﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class StudentNotFoundException : ApplicationException
    {
        public StudentNotFoundException() : base("The specified student does not exist.") { }
        public StudentNotFoundException(string message) : base(message) { }
    }
}
