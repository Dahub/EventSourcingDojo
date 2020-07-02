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
        private readonly ISpyier _spyier;

        public CreateZombieCommand(
            IEventBus eventBus,
            ISpyier spyier)
        {
            _eventBus = eventBus;
            _spyier = spyier;
        }

        public void Execute(
            Guid id,
            string name)
        {
            var zombie = new Zombie(_eventBus, id, name, MaxVitality, NumberOfMember);
            _spyier.Spy(zombie.ToString());
        }
    }
}
