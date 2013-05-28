using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion
{
    public class EventAggregator : IEventAggregator
    {
        private readonly List<IHandleEvents> _subscribers;

        public EventAggregator()
        {
            _subscribers = new List<IHandleEvents>();
        }

        public void Register(IHandleEvents handler)
        {
            _subscribers.Add(handler);
        }

        public void Publish(GameMessage @event)
        {
            _subscribers.ForEach(
                s =>
                    {
                        if (s.CanHandle(@event))
                            s.Handle(@event);
                    });
        }

    }
}