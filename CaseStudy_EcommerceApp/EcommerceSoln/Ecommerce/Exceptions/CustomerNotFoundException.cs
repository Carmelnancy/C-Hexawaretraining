﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Exceptions
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
