using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public class EventFilterPipeline : IHandleExternalEvents
    {
        public EventFilterPipeline(IHandleExternalEvents end)
        {
            Front = new ExternalEventFilter() { Next = end };
        }
        public void RegisterEventFilter(ExternalEventFilter eventFilter)
        {
            eventFilter.Next = this.Front;
            this.Front = eventFilter;
        }

        public void FilterMessage(IGameMessage message, IReactionScope scope, IHandleExternalEvents next)
        {
            Front.Handle(message, scope);
        }

        protected ExternalEventFilter Front { get; set; }
        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            Front.Handle(@event, scope);
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }
    }
}