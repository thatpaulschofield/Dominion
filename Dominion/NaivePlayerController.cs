using Dominion.GameEvents;

namespace Dominion
{
    public class NaivePlayerController : IPlayerController
    {
        public GameEventResponse HandleGameEvent(IMessage @event)
        {
            return @event.GetDefaultResponse();
        }
    }
}