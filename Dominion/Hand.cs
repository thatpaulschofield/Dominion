using Dominion.Cards;

namespace Dominion
{
    public class Hand : CardSet
    {
        public void Draw(Card card)
        {
            this.Add(card);
        }

        private new void Add(Card card)
        {
            base.Add(card);
        }

        private new void Remove(Card card)
        {
            base.Remove(card);
        }

        public void Discard(Card card, DiscardPile discardPile)
        {
            if (this.Contains(card))
            {
                Remove(card);
                discardPile.Discard(card);
            }
        }

        public void Discard(DiscardPile discardPile)
        {
            this.ForEach(c => Discard(c, discardPile));
        }
    }
}