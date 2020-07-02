namespace EventSourcingDojo.Infrastructure
{
    using Domain.Abstraction;

    public class OnFileEventStream : IEventStream
    {        
        private readonly IEventDatabase _eventDatabase;

        public OnFileEventStream(IEventDatabase eventRepository) 
            => _eventDatabase = eventRepository;

        public void AddEvent(IDomainEvent @event)
            => _eventDatabase.Save(@event);
    }
}
