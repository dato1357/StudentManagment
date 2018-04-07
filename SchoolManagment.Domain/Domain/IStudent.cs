using System;
using System.Collections.Generic;

namespace SchoolManagment.Domain.Domain
{
    public interface IStudent
    {
        int Age { get; }
        DateTimeOffset DateOfBirth { get; }
        string FirstName { get; }
        int Gpa { get; }
        string LastName { get; }
        List<Subject> Subjects { get; }

        IStudent CreateNewStudent(string firstName, string lastName, int age, DateTimeOffset dateOfBirth, int gpa);
    }
}