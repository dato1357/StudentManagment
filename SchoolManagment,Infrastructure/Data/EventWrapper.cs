using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Infrastructure.Data
{
    public class EventWrapper
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AggregateRootId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public Guid TransactionId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DomainEventType DomainEventType { get; set; }
        public string Data { get; set; }

        public EventWrapper(Guid aggregateId, DateTimeOffset dateCreated, int version, Guid transactionId, Guid userId, DomainEventType type, string data)
        {
            AggregateRootId = aggregateId;
            DateCreated = dateCreated;
            Version = version;
            TransactionId = transactionId;
            CreatedByUserId = userId;
            DomainEventType = type;
            Data = data;
        }
    }
}
