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

        public CardSet Hand { get;  set; }
        public CardSet Supply { get;  set; }
        public IEnumerable<Card> CardsInPlay { get;  set; }
        public int Actions { get;  set; }
        public int Buys { get;  set; }
        public int Coins { get;  set; }
        public AiContextGame Game { get;  set; }

        public bool ResponseIsAvailable(GameEventResponse response)
        {
            throw new System.NotImplementedException();
        }

        public IActionScope ActionScope { get; set; }
        public Money GetPrice(Card card)
        {
            throw new System.NotImplementedException();
        }

        public TurnState TurnState { get; private set; }
    }
}