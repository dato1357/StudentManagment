using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SchoolManagment.Domain.Shared
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        public int Version { get; internal set; }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(DomainEvent @event)
        {
            ApplyChange(@event, true);
        }
        private void ApplyChange(DomainEvent @event, bool isNew)
        {
            (this as dynamic).Apply(@event);
            if (isNew) _changes.Add(@event);
        }
    }
}
