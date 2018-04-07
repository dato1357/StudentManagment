using Queries.Persistence.Repositories;
using SchoolManagment.Domain.Contracts;
using SchoolManagment.Infrastructure.Data;

namespace Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolManagmentContext _context;

        public UnitOfWork(SchoolManagmentContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
        }

        public IStudentRepository Students { get; private set; }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}