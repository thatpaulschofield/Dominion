using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public interface IAiContext
    {
        IEnumerable<IEventResponse> AvailableResponses { get; }
        ResponseVotes Votes { get; }
        ResponseVotes VoteFor(IEventResponse first, int votes);
        CardSet Hand { get; set; }
        CardSet Supply { get; set; }
        IEnumerable<Card> CardsInPlay { get; }
        int Actions { get; set; }
        int Buys { get; set; }
        int Coins { get; set; }
        AiContextGame Game { get; set; }
        bool ResponseIsAvailable(GameEventResponse response);
        IActionScope ActionScope { get; set; }
        Money GetPrice(Card card);
    }

    public class AiContext : IAiContext
    {
        protected readonly IGameMessage _message;

        public AiContext(IGameMessage message)
        {
            _message = message;
        }

        public virtual IEnumerable<IEventResponse> AvailableResponses
        {
            get { return _message.GetAvailableResponses(); }
        }

        public ResponseVotes Votes { get { return new ResponseVotes(); }
        }

        public ResponseVotes VoteFor(IEventResponse first, int votes)
        {
            return new ResponseVotes().VoteFor(first, votes);
        }

        public CardSet Hand { get; set; }

        public CardSet Supply { get; set; }

        public IEnumerable<Card> CardsInPlay { get; set; }


        public int Actions { get; set; }

        public int Buys { get; set; }

        public int Coins { get; set; }

        public AiContextGame Game { get; set; }

        public bool ResponseIsAvailable(GameEventResponse response)
        {
            return _message.IsResponseAvailable(response);
        }

        public IActionScope ActionScope { get; set; }
        public Money GetPrice(Card card)
        {
            return ActionScope.GetPrice(card);
        }
    }
}