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
            lock(_subscribers)
                _subscribers.Add(handler);
        }

        public void Unregister(IHandleEvents handler)
        {
            lock (_subscribers)
                _subscribers.Remove(handler);
        }

        public void Publish(IGameMessage @event)
        {
            lock (_subscribers)
                _subscribers.ForEach(
                s =>
                    {
                        if (s.CanHandle(@event))
                        {
                            s.Handle(@event);
                        }
                    });
        }
    }
}