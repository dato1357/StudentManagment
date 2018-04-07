using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagment.Domain.Domain;

namespace SchoolManagment.Domain.Contracts
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetByGpa();
    }
}
