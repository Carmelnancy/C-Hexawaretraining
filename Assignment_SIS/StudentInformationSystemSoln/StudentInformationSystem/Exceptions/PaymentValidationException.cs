using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class PaymentValidationException : ApplicationException
    {
        public PaymentValidationException() : base("Payment validation failed due to invalid data.") { }
        public PaymentValidationException(string message) : base(message) { }
    }
}
