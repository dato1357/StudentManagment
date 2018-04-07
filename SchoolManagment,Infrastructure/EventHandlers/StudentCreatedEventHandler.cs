using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Domain.Events.Student;
using SchoolManagment.Domain.Shared;
using SchoolManagment.Infrastructure.ReadModels;

namespace SchoolManagment.Infrastructure.EventHandlers
{
    public class StudentCreatedEventHandler : IHandle<StudentCreatedEvent>
    {
        private readonly StudentReadDataStore _readStore;

        public StudentCreatedEventHandler(StudentReadDataStore readStore)
        {
            _readStore = readStore;
        }
        public void Handle(StudentCreatedEvent @event)
        {
            _readStore.Students.Add(new StudentReadModel(@event.Student.FirstName, @event.Student.LastName,@event.Student.Age));
            _readStore.SaveChanges();
        }
    }
}
