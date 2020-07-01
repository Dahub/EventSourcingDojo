namespace EventSourcingDojo.Domain.Abstraction
{
    public interface IEventStream
    {
        void AddEvent(IDomainEvent @event);
    }
}
