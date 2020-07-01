namespace EventSourcingDojo.Domain.Abstraction
{
    using System;

    public interface IDomainEvent
    {
        string AggregateName { get; }

        string EventName { get; }

        Guid AggregateId { get; }

        int AggregateVersion { get; }
    }
}
