using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class Hand : CardSet
    {
        public void Draw(Card card)
        {
            _innerList.Add(card);
        }

        public void Discard(Card card, DiscardPile discardPile)
        {
            if (this.Contains(card))
            {
                _innerList.Remove(card);
                discardPile.Discard(card);
            }
        }

        public void Discard(DiscardPile discardPile)
        {
            _innerList.ForEach(c => Discard(c, discardPile));
        }

        public void PlayCard(Card cardToPlay, CardSet cardsInPlay)
        {
            if (this.Contains(cardToPlay))
            {
                _innerList.Remove(cardToPlay);
                cardsInPlay.Add(cardToPlay);
            }
        }
    }
}