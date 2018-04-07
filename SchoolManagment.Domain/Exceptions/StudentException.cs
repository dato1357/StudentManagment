using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Exceptions
{
    public class StudentException : Exception
    {
        public StudentException() { }
        public StudentException(string message,object parameterInfo) : base(message) { }
        public StudentException(string message, Exception inner) : base(message, inner) { }
   
    }
}

