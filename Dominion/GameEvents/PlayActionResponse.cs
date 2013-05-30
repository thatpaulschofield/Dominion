using System;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayActionResponse : GameEventResponse
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
            return String.Format("{0} playing action {1}", TurnScope.Player.Name, Card.Name);
        }
    }
}