using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Exceptions
{
    public class InvalidLoanException : ApplicationException
    {
        public InvalidLoanException() : base("Provided loan id is invalid") { }

        public InvalidLoanException(string message) : base(message) { }
    }
}
