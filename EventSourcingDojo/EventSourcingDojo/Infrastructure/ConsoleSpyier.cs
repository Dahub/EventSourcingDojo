namespace EventSourcingDojo.Infrastructure
{
    using System;
    using Domain.Abstraction;

    public class ConsoleSpyier : ISpyier
    {
        public void Spy(string text) => Console.WriteLine(text);
    }
}
