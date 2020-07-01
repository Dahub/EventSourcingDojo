namespace EventSourcingDojo.Domain.Model.ZombieAggregate.Entities
{
    using System;
    using System.Collections.Generic;
    using EventSourcingDojo.Domain.Abstraction;
    using EventSourcingDojo.Domain.Model.ZombieAggregate.Events;

    public class Zombie
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int MaxVitality { get; private set; }

        public int NumberOfMember { get; private set; }

        public int Vitality { get; private set; }

        private int _version;

        private Zombie() { }

        public Zombie(
            IEventBus eventBus,
            Guid id,
            string name,
            int maxVitality,
            int numberOfMember)
        {
            if (numberOfMember > 4)
            {
                throw new Exception("trop de membres sur ce zombie !");
            }
            if (maxVitality < 0 || maxVitality > 10)
            {
                throw new Exception("vitalité maximum éronnée");
            }

            var humainRessuscited = new HumainRessuscited(
                this.GetType().Name,
                id,
                1,
                name,
                maxVitality,
                numberOfMember);

            eventBus.Push(humainRessuscited);

            Mutate(humainRessuscited);
        }

        public void WalkALongTime(IEventBus eventBus)
        {
            if (Vitality < 3)
            {
                throw new Exception("trop la flemme de bouger");
            }

            var walkedALongTime = new WalkedALongTime(
                this.GetType().Name,
                Id,
                _version + 1,
                Vitality - 3);

            eventBus.Push(walkedALongTime);

            Mutate(walkedALongTime);
        }

        public void EatBrain(IEventBus eventBus)
        {
            var breanEaten = new BrainEaten(
                this.GetType().Name,
                Id,
                _version + 1);

            eventBus.Push(breanEaten);

            Mutate(breanEaten);
        }

        public static Zombie Hydrate(IEnumerable<IDomainEvent> events)
        {
            var zombie = new Zombie();

            foreach(var @event in events)
            {
                zombie.Mutate(@event);
            }

            return zombie;
        }

        public override string ToString()
        {
            return @$"
Id : {Id}
Nom : {Name}
Vitalité : {Vitality}/{MaxVitality}";
        }

        private void Mutate(IDomainEvent @event)
        {
            if (@event.AggregateVersion != _version + 1)
            {
                throw new Exception(
                    $"Incohérence de version d'aggrégat, attendue : {_version + 1}, réelle : {@event.AggregateVersion}");
            }
            _version += 1;
            ApplyEvent(@event);
        }

        private void ApplyEvent(IDomainEvent @event)
        {
            switch (@event)
            {
                case HumainRessuscited evt:
                    Id = evt.AggregateId;
                    Name = evt.Name;
                    NumberOfMember = evt.NumberOfMember;
                    MaxVitality = evt.MaxVitalityLevel;
                    Vitality = evt.MaxVitalityLevel;
                    break;
                case WalkedALongTime evt:
                    Vitality = evt.VitalityLeft;
                    break;
                case BrainEaten _:
                    Vitality = MaxVitality;
                    break;
                default:
                    throw new Exception("Evenement non pris en charge par cette entité");
            };
        }
    }
}
