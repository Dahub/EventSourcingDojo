namespace EventSourcingDojo.Application
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Domain.Abstraction;
    using Domain.Model.ZombieAggregate.Entities;

    public class ZombieWalkCommand
    {
        private readonly IEventBus _eventBus;
        private readonly ISpyier _spyier;
        private readonly IEventDatabase _eventDataBase;

        public ZombieWalkCommand(
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
            zombie.WalkALongTime(_eventBus);
            _spyier.Spy(zombie.ToString());
        }
    }
}
