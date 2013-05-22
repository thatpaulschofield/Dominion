using Dominion.GameEvents;

namespace Dominion
{
    public class NaivePlayerController : IPlayerController
    {
        public GameEventResponse HandleGameEvent(GameEvent @event)
        {
            return @event.GetDefaultResponse();
        }
    }
}