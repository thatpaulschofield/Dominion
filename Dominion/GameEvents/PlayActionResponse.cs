using Dominion.AI;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayActionResponse : GameEventResponse
    {
        public PlayActionResponse(TurnScope turnScope, Card card) : base(turnScope)
        {
            this.Card = card;
        }

        public Card Card { get; private set; }
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}