using Dominion.GameEvents;

namespace Dominion.PlayerControllers
{
    public class AiPlayerController : IPlayerController
    {
        public IEventResponse HandleGameEvent(IGameMessage @event)
        {
            throw new System.NotImplementedException();
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope)
        {
            throw new System.NotImplementedException();
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            throw new System.NotImplementedException();
        }
    }
}