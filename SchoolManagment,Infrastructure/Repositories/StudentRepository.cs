using System;
using System.Collections.Generic;
using System.Linq;
using Queries.Persistence.Repositories;
using SchoolManagment.Domain.Contracts;
using SchoolManagment.Domain.Domain;
using SchoolManagment.Infrastructure.Data;

namespace Queries.Persistence
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly SchoolManagmentContext _context;

        public StudentRepository(SchoolManagmentContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetByGpa()
        {
            return _context.Students.OrderByDescending(s => s.Gpa).ToList();
        }
    }
}