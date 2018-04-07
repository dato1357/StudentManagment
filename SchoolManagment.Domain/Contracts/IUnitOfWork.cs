using System;

namespace SchoolManagment.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        int Commit();
    }
}