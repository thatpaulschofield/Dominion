using System;
using System.Linq;
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

        public Guid Id { get { return _ai.Id; } }

        public IEventResponse HandleGameEvent(IGameMessage @event, IActionScope scope)
        {
            if (!@event.GetAvailableResponses().Any())
                return @event.GetDefaultResponse(); 
            
            return _ai.HandleGameEvent(@event, scope);
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            return _ai.HandleGameEvent(@event, scope);
        }
    }
}