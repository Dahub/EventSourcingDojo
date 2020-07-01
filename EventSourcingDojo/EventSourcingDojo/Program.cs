namespace EventSourcingDojo
{
    using System;
    using System.IO;
    using System.Linq;
    using EventSourcingDojo.Domain.Model.ZombieAggregate.Entities;
    using EventSourcingDojo.Infrastructure;

    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("./EventsRepository/Events.txt");

            Console.WriteLine("Hello zombies!");

            var eventRepository = new OnFileEventRepository();
            var eventStream = new OnFileEventStream(eventRepository);
            var eventBus = new EventBus(eventStream);

            var zombie = new Zombie(
                eventBus,
                Guid.NewGuid(),
                "Alexandre",
                8,
                4);

            zombie.WalkALongTime(eventBus);

            var allEvents = eventRepository.GetEvents(zombie.Id).ToList();

            var newZombie = Zombie.Hydrate(allEvents);
        }
    }
}
