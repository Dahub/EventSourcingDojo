namespace EventSourcingDojo.Domain.Model.ZombieAggregate.Events
{
    using System;
    using EventSourcingDojo.Domain.Abstraction;

    public class HumainRessuscited : IDomainEvent
    {
        public HumainRessuscited(
            string aggregateName,
            Guid aggregateId,
            int aggregateVersion,
            string name,
            int maxVitalityLevel,
            int numberOfMember)
        {
            AggregateName = aggregateName;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            Name = name;
            MaxVitalityLevel = maxVitalityLevel;
            NumberOfMember = numberOfMember;
        }

        public string AggregateName { get; private set; }

        public Guid AggregateId { get; private set; }

        public int AggregateVersion { get; private set; }

        public string EventName => this.GetType().FullName;

        public string Name { get; private set; }

        public int MaxVitalityLevel { get; set; }

        public int NumberOfMember { get; set; }
    }
}
