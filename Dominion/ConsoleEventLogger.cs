using System;
using Dominion.GameEvents;

namespace Dominion
{
    public class ConsoleEventLogger : IHandleEvents
    {
        public ConsoleEventLogger(IEventAggregator eventAggregator)
        {
            eventAggregator.Register(this);
        }

        public void Handle(IGameMessage @event)
        {
            Console.WriteLine("\t[{0}]", @event.ActionScope);
            Console.WriteLine("\t[{0}]", @event);

            if (@event is GameEndedEvent)
            {
                Console.WriteLine("Press a key to continue...");
                Console.ReadKey();
            }
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }
    }
}