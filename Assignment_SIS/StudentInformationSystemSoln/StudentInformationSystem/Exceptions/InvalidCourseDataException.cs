﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InvalidCourseDataException : ApplicationException
    {
        public InvalidCourseDataException() : base("Invalid data provided for course creation or update.") { }
        public InvalidCourseDataException(string message) : base(message) { }
    }
}
