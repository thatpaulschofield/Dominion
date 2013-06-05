using System.Collections.Generic;
using Dominion.AI;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.Tests.AI
{
    public class MockAiContext : IAiContext
    {
        public MockAiContext()
        {
            AvailableResponses = new List<IEventResponse>();
        }
        public IEnumerable<IEventResponse> AvailableResponses { get; set; }
        public ResponseVotes Votes { get; private set; }

        public ResponseVotes VoteFor(IEventResponse first, int votes)
        {
            return new ResponseVotes();
        }

        public CardSet Hand { get; private set; }
        public CardSet Supply { get; private set; }
        public IEnumerable<Card> CardsInPlay { get; private set; }
        public int Actions { get; private set; }
        public int Buys { get; private set; }
        public int Coins { get; private set; }
        public AiContextGame Game { get; private set; }

        public bool ResponseIsAvailable(GameEventResponse response)
        {
            throw new System.NotImplementedException();
        }

        public TurnState TurnState { get; private set; }
    }
}