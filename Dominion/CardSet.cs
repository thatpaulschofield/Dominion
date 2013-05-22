using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class CardSet : List<Card>
    {
        public CardSet()
        {
        }

        public CardSet(IEnumerable<Card> cards)
        {
            this.AddRange(cards);
        }

        public CardSet(params Card[] cards) : this(cards.ToList())
        {
        }

        public CardSet Treasures()
        {
            return new CardSet(this.Where(c => c.IsTreasure));
        }

        public CardSet this[CardType type]
        {
            get { return new CardSet(this.Where(c => c.CardType == type)); }
        }

        public void Into(Hand hand)
        {
            this.ForEach(c => hand.Draw(c));
        }

        public void DiscardInto(DiscardPile discardPile)
        {
            this.ToList().ForEach(card =>
                {
                    this.Remove(card);
                    discardPile.Discard(card);
                });
        }

        public static implicit operator CardSet(Card card)
        {
            return new CardSet(card);
        }
    }
}