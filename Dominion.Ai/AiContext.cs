using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public interface IAiContext
    {
        IEnumerable<IEventResponse> AvailableResponses { get; }
        ResponseVotes Votes { get; }
        ResponseVotes VoteFor(IEventResponse first, int votes);
    }

    public class AiContext : IAiContext
    {
        private readonly IGameMessage _message;

        public AiContext(IGameMessage message)
        {
            _message = message;
        }

        public IEnumerable<IEventResponse> AvailableResponses
        {
            get { return _message.GetAvailableResponses(); }
        }

        public ResponseVotes Votes { get { return new ResponseVotes(); }
        }

        public ResponseVotes VoteFor(IEventResponse first, int votes)
        {
            return new ResponseVotes().VoteFor(first, votes);
        }
    }
}