using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;
using Should;

namespace Dominion.Tests.GameEvents
{
    public class MockEventAggregator : IEventAggregator
    {
        private readonly List<IGameMessage> _publishedEvents = new List<IGameMessage>();

        public MockEventAggregator()
        {
        }

        public void Register(IHandleEvents handler)
        {
            
        }

        public void Unregister(IHandleEvents handler)
        {
            
        }

        public void Publish(IGameMessage @event)
        {
            _publishedEvents.Add(@event);
            Console.WriteLine("MockEventAggregator: event published: {0}, {1}, {2}", @event.Id, @event.GetType().Name, @event);
        }

        public IEnumerable<IGameMessage> PublishedEvents
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

            string typesSent = _publishedEvents.Aggregate("", (s, message) => s += (message.GetType().Name + " "));
            Contains<T>().ShouldBeTrue(String.Format("Message type {0} was not sent, but should have been. Sent messages: [{1}]", typeof(T).Name, typesSent));
        }

        public T FindMessage<T>() where T:class
        {
            return _publishedEvents.FirstOrDefault(e => e.GetType() == typeof (T)) as T;
        }

        public void AssertMessageWasNotSent<T>()
        {
            Contains<T>().ShouldBeFalse(String.Format("Message type {0} was sent, but should not have been.", typeof(T).Name));
        }
    }
}