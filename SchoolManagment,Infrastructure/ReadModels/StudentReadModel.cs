using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Infrastructure.ReadModels
{
    public class StudentReadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }


        public StudentReadModel(string firstname, string lastname, int? age)
        {
            FirstName = firstname;
            LastName = lastname;
            Age = age;
        }
    }
}
