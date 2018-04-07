using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Domain.Commands
{
    public class CreateStudentCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public int Age { get; set; }
    }
}
