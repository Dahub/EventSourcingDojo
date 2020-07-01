namespace EventSourcingDojo.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using EventSourcingDojo.Domain.Abstraction;

    public class OnFileEventRepository : IEventRepository
    {
        private const string EventRepositoryPath = "./EventsRepository";
        private const string EventRepositoryFile = "Events.txt";

        public void Save(IDomainEvent @event)
        {
            CreateRepositoryIfNotExists();

            string inlinedEvent = Inline(@event);

            File.AppendAllLines(EventFilePath(), new string[] { inlinedEvent });
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId)
        {
            var lines = File.ReadAllLines(EventFilePath()).Where(l => l.StartsWith(aggregateId.ToString()));

            var serializer = new JSonSerializer();
            foreach(var line in lines)
            {
                var lineDetails = line.Split("##", StringSplitOptions.RemoveEmptyEntries);
                yield return (IDomainEvent)serializer.Deserialize(GetType(lineDetails[1]), lineDetails[4]);
            }
        }

        private string Inline(IDomainEvent @event)
            => $"{@event.AggregateId}##{@event.EventName}##{@event.AggregateName}##{@event.AggregateVersion}##{Serialize(@event)}";

        private static void CreateRepositoryIfNotExists()
        {
            if (!Directory.Exists(EventRepositoryPath))
            {
                Directory.CreateDirectory(EventRepositoryPath);
            }
            if (!File.Exists(EventRepositoryFile))
            {
                File.Create(EventRepositoryFile);
            }
        }

        private string Serialize(IDomainEvent @event) => new JSonSerializer().Serialize(@event);

        private string EventFilePath()
            => $"{EventRepositoryPath}/{EventRepositoryFile}";

        private Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
