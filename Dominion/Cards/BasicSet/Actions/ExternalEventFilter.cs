using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class ExternalEventFilter : IHandleExternalEvents
    {
        public IHandleExternalEvents Next { get; set; }

        public ExternalEventFilter()
        {
            
        }

        public ExternalEventFilter(Func<IGameMessage, bool> filter)
        {
            Filter = filter;
        }

        protected Func<IGameMessage, bool> Filter = (message) => true;


        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            if (Next != null)
            {
                Next.Handle(@event, scope);
            }
        }
    }
}