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
        CardSet Hand { get; }
        CardSet Supply { get; }
        IEnumerable<Card> CardsInPlay { get; }
        int Actions { get; }
        int Buys { get; }
        int Coins { get; }
        AiContextGame Game { get; }
        bool ResponseIsAvailable(GameEventResponse response);
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

        public CardSet Hand { get { return new CardSet(_message.TurnScope.Hand);} }
        public CardSet Supply { get { return new CardSet(_message.TurnScope.Supply.Select(p => p.Value.Type.Create()).ToArray());} }
        public IEnumerable<Card> CardsInPlay { get{ return new CardSet(_message.TurnScope.CardsInPlay);} }
        public int Actions { get { return _message.TurnScope.Actions; } }
        public int Buys { get { return _message.TurnScope.Buys; } }
        public int Coins { get { return _message.TurnScope.Coins; } }
        public AiContextGame Game { get; private set; }

        public bool ResponseIsAvailable(GameEventResponse response)
        {
            return _message.IsResponseAvailable(response);
        }
    }
}