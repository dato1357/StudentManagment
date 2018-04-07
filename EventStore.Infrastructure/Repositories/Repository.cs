using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SchoolManagment.Domain.Contracts;
using SchoolManagment.Domain.Shared;
using SchoolManagment.Infrastructure.Data;

namespace EventStore.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : AggregateRoot, new()
    {
        private readonly SchoolManagmentContext _storage;

        public Repository(SchoolManagmentContext storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            var nextVersion = expectedVersion;
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                nextVersion++;
                if (aggregate.Version != expectedVersion) throw new Exception("concurrency conflict occured");
                _storage.DomainEvents.Add(new EventWrapper(@event.AggregateRootId, @event.DateCreated,
                    nextVersion,
                    @event.TransactionId,
                    @event.CreatedByUserId,
                    @event.DomainEventType,
                    JsonConvert.SerializeObject(@event)
                ));
            }
        }

        public T GetById(Guid id)
        {
            var obj = new T(); 
            var events = _storage.DomainEvents.OrderBy(e=>e.DateCreated).Where(e => e.AggregateRootId == id).ToList();
            var deserializedEvents = new List<DomainEvent>();
            foreach (var @event in events)
            {
                deserializedEvents.Add(JsonConvert.DeserializeObject<DomainEvent>(@event.Data));
            }
            obj.LoadsFromHistory(deserializedEvents);
            return obj;
        }

        public List<DomainEvent> GetEventsByType(DomainEventType t)
        {
            var events = _storage.DomainEvents.Where(e => e.DomainEventType == t).ToList();
            var deserializedEvents = new List<DomainEvent>();
            foreach (var @event in events)
            {
                deserializedEvents.Add(JsonConvert.DeserializeObject<DomainEvent>(@event.Data));
            }
           
            return deserializedEvents;
        }

        public T GetByStreamPosition(Guid aggregateId, int? startPosition, int? endPosition, int count)
        {
            var obj = new T();
            var events = new List<EventWrapper>();
            if (startPosition.HasValue)
            {
                 events = _storage.DomainEvents.OrderBy(e=>e.DateCreated).Where(e => e.AggregateRootId == aggregateId)
                    .Where(p => p.Version >= startPosition).ToList();
            }
            if (endPosition.HasValue)
            {
                 events = _storage.DomainEvents.Where(e => e.AggregateRootId == aggregateId)
                    .Where(p => p.Version <= endPosition).ToList();
            }
            var deserializedEvents = new List<DomainEvent>();
            foreach (var @event in events)
            {
                deserializedEvents.Add(JsonConvert.DeserializeObject<DomainEvent>(@event.Data));
            }
            obj.LoadsFromHistory(deserializedEvents);
            return obj;
        }

        public List<DomainEvent> GetEventsByDescendingOrder(Guid aggregateId, int? startPosition, int? endPosition, int count)
        {
            var obj = new T();
            var events = new List<EventWrapper>();
            if (startPosition.HasValue)
            {
                events = _storage.DomainEvents.OrderByDescending(e => e.DateCreated).Where(e => e.AggregateRootId == aggregateId)
                    .Where(p => p.Version >= startPosition).ToList();
            }
            if (endPosition.HasValue)
            {
                events = _storage.DomainEvents.Where(e => e.AggregateRootId == aggregateId)
                    .Where(p => p.Version <= endPosition).ToList();
            }
            var deserializedEvents = new List<DomainEvent>();
            foreach (var @event in events)
            {
                deserializedEvents.Add(JsonConvert.DeserializeObject<DomainEvent>(@event.Data));
            }
            return deserializedEvents;
        }
    }
}