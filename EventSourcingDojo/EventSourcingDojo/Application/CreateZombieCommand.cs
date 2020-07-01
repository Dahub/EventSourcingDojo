namespace EventSourcingDojo.Application
{
    using System;
    using EventSourcingDojo.Domain.Abstraction;
    using EventSourcingDojo.Domain.Model.ZombieAggregate.Entities;

    public class CreateZombieCommand
    {
        private const int MaxVitality = 10;
        private const int NumberOfMember = 4;
        private readonly IEventBus _eventBus;

        public CreateZombieCommand(
            IEventBus eventBus) => _eventBus = eventBus;

        public void Execute(
            Guid id,
            string name)
            => new Zombie(_eventBus, id, name, MaxVitality, NumberOfMember);
    }
}
