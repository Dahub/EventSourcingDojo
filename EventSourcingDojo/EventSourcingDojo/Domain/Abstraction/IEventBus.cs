namespace EventSourcingDojo.Domain.Abstraction
{
    public interface IEventBus
    {
        void Push(IDomainEvent @event);
    }
}
