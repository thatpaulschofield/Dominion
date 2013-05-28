using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class Hand : CardSet
    {
        private readonly IEventAggregator _eventAggregator;

        public Hand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Draw(Card card)
        {
            _innerList.Add(card);
        }

        protected override void OnCardAdded(Card card, ITurnScope turnScope)
        {
            _eventAggregator.Publish(new CardDrawnEvent(card, turnScope));
        }

        public void Discard(Card card, DiscardPile discardPile, ITurnScope turnScope)
        {
            if (this.Contains(card))
            {
                _innerList.Remove(card);
                discardPile.Discard(card, turnScope);
            }
        }

        public void Discard(DiscardPile discardPile, ITurnScope turnScope)
        {
            _innerList.ToList().ForEach(c => Discard(c, discardPile, turnScope));
        }

        public void PlayCard(Card cardToPlay, CardSet cardsInPlay, ITurnScope turnScope)
        {
            if (this.Contains(cardToPlay))
            {
                _innerList.Remove(cardToPlay);
                cardsInPlay.Add(cardToPlay, turnScope);
            }
        }

        public void PlayAction(Card cardToPlay, ITurnScope turnScope)
        {
            if (this.Contains(cardToPlay))
            {
                turnScope.PlayAction(cardToPlay);
            }
        }

        public void PlayTreasure(Card treasure, TurnScope turnScope)
        {
            if (this.Contains(treasure))
            {
                _innerList.Remove(treasure);
                turnScope.PlayTreasure(treasure);
            }
        }

    }
}