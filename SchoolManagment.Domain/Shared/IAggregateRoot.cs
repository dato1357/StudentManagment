using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Shared
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        int Version { get; }

        List<DomainEvent> GetUncommittedEvents();

        void MarkEventsAsCommitted();

        void LoadFromHistory(IEnumerable<DomainEvent> events);
    }
}
