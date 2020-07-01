namespace EventSourcingDojo.Domain.Model.ZombieAggregate.Events
{
    using System;
    using EventSourcingDojo.Domain.Abstraction;

    public class BrainEaten : IDomainEvent
    {
        public BrainEaten(
           string aggregateName,
           Guid aggregateId,
           int aggregateVersion)
        {
            AggregateName = aggregateName;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;            
        }

        public string AggregateName { get; private set; }

        public Guid AggregateId { get; private set; }

        public int AggregateVersion { get; private set; }

        public string EventName => this.GetType().FullName;
    }
}
