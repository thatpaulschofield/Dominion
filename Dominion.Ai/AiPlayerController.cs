using Dominion.GameEvents;

namespace Dominion.Ai
{
    public class AiPlayerController : IPlayerController
    {
        private readonly AI.AiStrategy _ai;

        public AiPlayerController(AI.AiStrategy ai)
        {
            _ai = ai;
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope)
        {
            return _ai.HandleGameEvent(@event, scope);
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            return _ai.HandleGameEvent(@event, scope);
        }
    }
}