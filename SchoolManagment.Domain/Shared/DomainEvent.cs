using System;

namespace SchoolManagment.Domain.Shared
{
    public class DomainEvent
    {
        public Guid AggregateRootId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int  ExpectedVersion { get; set; }
        public Guid TransactionId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DomainEventType DomainEventType { get; set; }

        public DomainEvent(Guid aggregateId,DomainEventType domainEventType)
        {
            AggregateRootId = aggregateId;
            DateCreated = DateTimeOffset.Now;
            TransactionId = Guid.NewGuid();
            DomainEventType = domainEventType;
        }
    }
}