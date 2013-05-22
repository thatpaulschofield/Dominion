using System;

namespace Dominion
{
    public class TurnScope
    {
        private CardSet _cardsToDiscard;
        private DiscardPile _discardPile;

        public TurnScope(Supply supply, DiscardPile discardPile)
        {
            if (discardPile == null)
                throw new ArgumentNullException("DiscardPile cannot be null");
            _discardPile = discardPile;
            if (supply == null)
                throw new ArgumentNullException("Supply cannot be null.");

            Supply = supply;
            _cardsToDiscard = new CardSet();
        }

        public void Discard(CardSet cardsToDiscard)
        {
            _cardsToDiscard.AddRange(cardsToDiscard);
        }

        public Supply Supply { get; private set; }

        public void CleanUp()
        {
            _cardsToDiscard.DiscardInto(_discardPile);
        }
    }
}