using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Commands;
using SchoolManagment.Domain.Contracts;
using SchoolManagment.Domain.Domain;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Domain.CommandHandlers
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand>
    {
        private readonly IRepository<Student> _repo;

        public CreateStudentCommandHandler(IRepository<Student> repo)
        {
            _repo = repo;
        }
        public CommandResult Execute(CreateStudentCommand command)
        {
            var student = new Student();
            student.CreateNewStudent(command.FirstName, command.LastName, command.Age, command.DateOfBirth, 1);
            _repo.Save(student, -1);
            return new CommandResult(CommandResultStatus.Success, "სტუდენტი შეიქმნა!");
        }
    }
}
