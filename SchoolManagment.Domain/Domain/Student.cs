using SchoolManagment.Domain.Exceptions;
using SchoolManagment.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Domain.Events.Student;

namespace SchoolManagment.Domain.Domain
{
    public class Student : AggregateRoot, IStudent
    {
        public Student()
        {
            Subjects = new List<Subject>();
        }
        public Student(string firstName, string lastName,int age,DateTimeOffset dateOfBirth,int gpa)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DateOfBirth = dateOfBirth;
            Gpa = gpa;
        }

        public static readonly NullStudent NullStudent = new NullStudent();

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public DateTimeOffset DateOfBirth { get; private set; }
        public int Gpa { get; private set; }
        public List<Subject> Subjects { get; private set; }

        public  IStudent CreateNewStudent(string firstName, string lastName, int age, DateTimeOffset dateOfBirth, int gpa)
        {
            if (IsValid())
            {
                var newStudent =  new Student(firstName, lastName, age, dateOfBirth, gpa);
                ApplyChange(new StudentCreatedEvent(newStudent));
                return newStudent;
            }

            return NullStudent;
        }

        private void Apply(StudentCreatedEvent e)
        {
            FirstName = e.Student.FirstName;
            LastName = e.Student.LastName;
            Age = e.Student.Age;
            Gpa = e.Student.Gpa;
            Subjects = e.Student.Subjects;
            DateOfBirth = e.Student.DateOfBirth;
        }

       

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new StudentException("Name is required",new {FirstName = FirstName});
                return true;
        }
    }
}
