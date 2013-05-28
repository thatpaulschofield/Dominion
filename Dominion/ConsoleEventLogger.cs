using System;
using Dominion.GameEvents;

namespace Dominion
{
    public class ConsoleEventLogger : IHandleEvents<IMessage>
    {
        public ConsoleEventLogger(IEventAggregator eventAggregator)
        {
            eventAggregator.Register(this);
        }

        public void Handle(IMessage @event)
        {
            Console.WriteLine("\t[{0}]", @event.TurnScope);
            Console.WriteLine("\t[{0}]", @event);
        }

        void IHandleEvents<IMessage>.Handle(IMessage @event)
        {
            this.Handle(@event);
        }

        public bool CanHandle(IMessage @event)
        {
            return true;
        }
    }
}