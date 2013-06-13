using System;
using AutoMapper;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public class AiStrategy
    {
        private readonly Function<ResponseVotes> _root;
        public AiStrategyId Id { get; set; }

        public AiStrategy(Function<ResponseVotes> root)
        {
            _root = root;
            Id = new AiStrategyId();
        }

        public AiStrategy(Function<ResponseVotes> root, AiStrategyId id)
        {
            _root = root;
            Id = id;
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IActionScope scope)
        {
            var aiContext = Mapper.Map<IGameMessage, AiContext>(@event);
            var votes = _root.Evaluate(aiContext);
            votes = votes.VoteFor(@event.GetDefaultResponse(), 1);
            return votes.Winner;
        }

        public override string ToString()
        {
            return "Ai Strategy: " + Id;
        }

        public class AiStrategyId : Id<Guid>
        {
            public AiStrategyId() : base(Guid.NewGuid())
            {
            }

            public AiStrategyId(Guid id) : base(id)
            {
            }

            public static implicit operator AiStrategyId(Guid id)
            {
                return new AiStrategyId(id);
            }

            public static implicit operator Player.PlayerId(AiStrategyId id)
            {
                return new Player.PlayerId(id._id);
            }
        }
    }

}