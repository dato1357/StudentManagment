using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Domain
{
    public class NullStudent : IStudent
    {
        public int Age => 0;

        public DateTimeOffset DateOfBirth => DateTime.MinValue;

        public string FirstName => string.Empty;

        public int Gpa => 0;

        public string LastName => string.Empty;

        public List<Subject> Subjects => new List<Subject>();

        public IStudent CreateNewStudent(string firstName, string lastName, int age, DateTimeOffset dateOfBirth, int gpa)
        {
            return this;
        }

    }
}
