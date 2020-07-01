namespace EventSourcingDojo.Infrastructure
{
    using System.IO;
    using System.Text.Json;
    using EventSourcingDojo.Domain.Abstraction;

    public class OnFileEventStream : IEventStream
    {        
        private readonly IEventRepository _eventRepository;

        public OnFileEventStream(IEventRepository eventRepository) 
            => _eventRepository = eventRepository;

        public void AddEvent(IDomainEvent @event)
            => _eventRepository.Save(@event);
    }
}
