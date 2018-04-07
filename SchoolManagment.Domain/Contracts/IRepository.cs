using SchoolManagment.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SchoolManagment.Domain.Contracts
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
        List<DomainEvent> GetEventsByType(DomainEventType t);
        T GetByStreamPosition(Guid aggregateId, int? startPositio, int? endPosition, int count);
    }
}
