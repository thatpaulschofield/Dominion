using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IPlayerController
    {
        IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope);
        IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope);
    }
}