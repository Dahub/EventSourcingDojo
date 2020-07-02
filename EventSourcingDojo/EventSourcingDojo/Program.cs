namespace EventSourcingDojo
{
    using System;
    using Application;
    using EventSourcingDojo.Infrastructure;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello zombies!");

            var eventDatabase = new OnFileEventDatabase();
            var eventStream = new OnFileEventStream(eventDatabase);
            var eventBus = new EventBus(eventStream);
            var spyier = new ConsoleSpyier();

            eventDatabase.ResetDatabase();

            var zombieId = Guid.NewGuid();
            var zombieName = "Alexandre";

            new CreateZombieCommand(
                eventBus,
                spyier).Execute(zombieId, zombieName);

            new ZombieWalkCommand(
                eventBus,
                spyier,
                eventDatabase).Execute(zombieId);

            new ZombieEatBrainCommand(
                eventBus,
                spyier,
                eventDatabase).Execute(zombieId);
        }
    }
}
