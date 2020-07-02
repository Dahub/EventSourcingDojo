namespace EventSourcingDojo.Application
{
    using System;
    using Domain.Abstraction;
    using Domain.Model.ZombieAggregate.Entities;

    public class ZombieEatBrainCommand
    {
        private readonly IEventBus _eventBus;
        private readonly ISpyier _spyier;
        private readonly IEventDatabase _eventDataBase;

        public ZombieEatBrainCommand(
            IEventBus eventBus,
            ISpyier spyier,
            IEventDatabase eventDataBase)
        {
            _eventBus = eventBus;
            _spyier = spyier;
            _eventDataBase = eventDataBase;
        }

        public void Execute(Guid zombieId)
        {
            var zombie = Zombie.Hydrate(_eventDataBase.GetEvents(zombieId));
            zombie.EatBrain(_eventBus);
            _spyier.Spy(zombie.ToString());
        }
    }
}
