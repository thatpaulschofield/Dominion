using AutoMapper;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public class AiStrategy
    {
        private Function<ResponseVotes> _root;

        public AiStrategy(Function<ResponseVotes> root)
        {
            _root = root;
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IActionScope scope)
        {
            var aiScope = Mapper.Map<IActionScope, AiContext>(scope);
            var votes = _root.Evaluate(aiScope);
            return votes.Winner;
        }
    }
}