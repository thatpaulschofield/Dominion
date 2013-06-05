using System;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayActionResponse : GameEventResponse<ActionPhase>
    {
        public PlayActionResponse(ITurnScope turnScope, Card card) : base(turnScope)
        {
            if (card == null)
                throw new ArgumentNullException("card");
            this.Card = card;
            this.Description = card.Name;
        }

        public Card Card { get; private set; }
        public override void Execute()
        {
            TurnScope.PlayAction(Card);
        }

        public override string ToString()
        {
            return String.Format("{0} playing action {1}", TurnScope.ActingPlayer.Name, Card.Name);
        }
    }
}