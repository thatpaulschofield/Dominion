using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public interface IHandleExternalEvents
    {
        void Handle(IGameMessage @event, IReactionScope scope);
    }
}