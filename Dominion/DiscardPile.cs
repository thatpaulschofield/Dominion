using Dominion.Cards;

namespace Dominion
{
    public class DiscardPile : CardSet
    {
        public void Discard(Card card)
        {
            this.Add(card);
        }

        private new void Add(Card card)
        {
            base.Add(card);
        }
    }
}