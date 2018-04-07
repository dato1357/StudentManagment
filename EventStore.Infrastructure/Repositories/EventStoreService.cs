using SchoolManagment.Domain.Shared;
using SchoolManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EventStore.Infrastructure.Repositories
{
    public abstract class EventStoreService
    {
        //There is a max limit of 4096 messages per read in eventstore so use paging
        private const int EventStorePageSize = 200;

        public async Task<IEnumerable<DomainEvent>> GetEventsAsync(Type aggregateType, Guid aggregateId, int start, int count)
        {

            var connection = GetEventStoreConnection();
            var events = await ReadEvents(aggregateType, connection, aggregateId, start, count);

            return events;
        }

        protected async Task<IEnumerable<DomainEvent>> ReadEvents(Type aggregateType, SchoolManagmentContext connection, Guid aggregateId, int start, int count)
        {

            var events = new List<DomainEvent>();
            var streamEvents = new List<DomainEvent>();
            StreamEventsSlice currentSlice;
            var nextSliceStart =  streamEvents.OrderByDescending(e=>e.DateCreated).Last().ExpectedVersion;

            //Read the stream using pagesize which was set before.
            //We only need to read the full page ahead if expected results are larger than the page size

            do
            {
                int nextReadCount = count - streamEvents.Count();

                if (nextReadCount > EventStorePageSize)
                {
                    nextReadCount = EventStorePageSize;
                }

                var domainEvents = connection.DomainEvents.Where(e => e.AggregateRootId == aggregateId).Where(e=>e.ExpectedVersion >=0).Take(count);

                streamEvents.AddRange(currentSlice.Events);

            } while (!currentSlice.IsEndOfStream);

            //Deserialize and add to events list
            foreach (var returnedEvent in streamEvents)
            {
                events.Add(DeserializeEvent(returnedEvent));
            }

            return events;
        }

        public async Task<DomainEvent> GetLastEventAsync(Type aggregateType, Guid aggregateId)
        {
            var connection = GetEventStoreConnection();

            var results = await connection.ReadStreamEventsBackwardAsync(
                $"{AggregateIdToStreamName(aggregateType, aggregateId)}", StreamPosition.End, 1, false);

            if (results.Status == SliceReadStatus.Success && results.Events.Count() > 0)
            {
                return DeserializeEvent(results.Events.First());
            }
            else
            {
                return null;
            }
        }

        public async Task<DomainEvent> CommitChangesAsync(AggregateRoot aggregate)
        {
            var connection = GetEventStoreConnection();
            var events = aggregate.GetUncommittedChanges();

            if (events.Any())
            {
                var lastVersion = aggregate.Version;
                List<EventData> lstEventData = new List<EventData>();

                foreach (var @event in events)
                {
                    lstEventData.Add(SerializeEvent(@event, aggregate.LastCommittedVersion + 1));
                }

                await connection.AppendToStreamAsync($"{AggregateIdToStreamName(aggregate.GetType(), aggregate.Id)}",
                    (lastVersion < 0 ? ExpectedVersion.NoStream : lastVersion), lstEventData);
            }
        }

        private SchoolManagmentContext GetEventStoreConnection()
        {

            var optionsBuilder = new DbContextOptionsBuilder<SchoolManagmentContext>();
            DbContextOptions<SchoolManagmentContext> contextOptions;
            contextOptions = optionsBuilder.UseMemoryCache(new MemoryCache(new MemoryCacheOptions())).Options;
            return new SchoolManagmentContext(contextOptions);
        }

    }
}
