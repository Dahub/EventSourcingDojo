namespace EventSourcingDojo.Infrastructure
{
    using EventSourcingDojo.Domain.Abstraction;

    public class EventBus : IEventBus
    {
        private readonly IEventStream _eventStream;

        public EventBus(IEventStream eventStream) => _eventStream = eventStream;

        public void Push(IDomainEvent @event)
            => _eventStream.AddEvent(@event);
    }
}
