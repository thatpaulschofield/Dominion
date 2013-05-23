using Dominion.GameEvents;

namespace Dominion
{
    public interface IPlayerController
    {
        GameEventResponse HandleGameEvent(GameEvent @event);
    }
}