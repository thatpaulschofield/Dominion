using System;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class DiscardPile : CardSet
    {
        public void Discard(Card card, ITurnScope turnScope)
        {
            this.Add(card, turnScope);
            turnScope.Publish(new DiscardedEvent(card, turnScope));
        }

        private new void Add(Card card, ITurnScope turnScope)
        {
            base.Add(card, turnScope);
        }
    }

    public class DiscardedEvent : GameMessage
    {
        private readonly Card _card;

        public DiscardedEvent(Card card, ITurnScope turnScope) : base(turnScope)
        {
            _card = card;
        }

        public override string ToString()
        {
            return String.Format("{0} discarded", _card.Name);
        }
    }
}