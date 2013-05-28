using System;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class TreasurePlayedEvent : GameMessage
    {
        private readonly Card _card;

        public TreasurePlayedEvent(Card card, ITurnScope turnScope) : base(turnScope)
        {
            _card = card;
        }

        public override string ToString()
        {
            return String.Format("{0} played\n{1}", _card, TurnScope);
        }
    }
}