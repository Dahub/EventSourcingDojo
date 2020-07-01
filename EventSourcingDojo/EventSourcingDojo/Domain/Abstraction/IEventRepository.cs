namespace EventSourcingDojo.Domain.Abstraction
{
    using System;
    using System.Collections.Generic;

    public interface IEventRepository
    {
        void Save(IDomainEvent @event);

        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
    }
}
