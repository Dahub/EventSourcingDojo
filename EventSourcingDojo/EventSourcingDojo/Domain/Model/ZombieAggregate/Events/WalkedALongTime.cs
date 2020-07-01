namespace EventSourcingDojo.Domain.Model.ZombieAggregate.Events
{
    using System;
    using EventSourcingDojo.Domain.Abstraction;

    public class WalkedALongTime : IDomainEvent
    {
        public WalkedALongTime(
           string aggregateName,
           Guid aggregateId,
           int aggregateVersion,           
           int vitalityLeft)
        {
            AggregateName = aggregateName;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            VitalityLeft = vitalityLeft;
        }

        public string AggregateName { get; private set; }

        public Guid AggregateId { get; private set; }

        public int AggregateVersion { get; private set; }

        public string EventName => this.GetType().FullName;

        public int VitalityLeft { get; private set; }
    }
}
