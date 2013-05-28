using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;
using Should;

namespace Dominion.Tests.GameEvents
{
    public class MockEventAggregator : IEventAggregator
    {
        private readonly List<GameMessage> _publishedEvents = new List<GameMessage>();

        public void Register(IHandleEvents handler)
        {
            
        }

        public void Publish(GameMessage @event)
        {
            _publishedEvents.Add(@event);
        }

        public IEnumerable<GameMessage> PublishedEvents
        {
            get { return _publishedEvents.AsReadOnly(); }
        }

        public bool Contains<T>()
        {
            var contains = _publishedEvents.Any(e => e.GetType() == typeof (T));
            return contains;
        }

        public void AssertMessageWasSent<T>()
        {
            Contains<T>().ShouldBeTrue();
        }

        public T FindMessage<T>() where T:class
        {
            return _publishedEvents.FirstOrDefault(e => e.GetType() == typeof (T)) as T;
        }
    }
}