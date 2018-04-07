using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Domain.Domain.Events.Student
{
    public class StudentCreatedEvent : DomainEvent
    {
        public Domain.Student Student { get; set; }

        public StudentCreatedEvent(Domain.Student student):base(student.Id,DomainEventType.StudentCreated)
        {
            Student = student;
        }
    }
}
