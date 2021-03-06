using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public class Hand : CardSet, IHandleExternalEvents, IHandleInternalEvents
    {
        public Hand() : base()
        {
        }

        public Hand(IEnumerable<Card> cards) : base(cards)
        {
            
        }

        public void Draw(Card card)
        {
            InnerList.Add(card);
        }

        protected override void OnCardAdded(Card card, IActionScope turnScope)
        {
            turnScope.Publish(new CardDrawnEvent(card, turnScope));
        }

        public void Discard(Card card, DiscardPile discardPile, IActionScope scope)
        {
            if (this.Contains(card))
            {
                InnerList.Remove(card);
                discardPile.Discard(card, scope);
            }
        }

        public void Discard(DiscardPile discardPile, IActionScope turnScope)
        {
            InnerList.ToList().ForEach(c => Discard(c, discardPile, turnScope));
        }

        public void PlayCard(Card cardToPlay, CardSet cardsInPlay, IActionScope turnScope)
        {
            if (this.Contains(cardToPlay))
            {
                InnerList.Remove(cardToPlay);
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
                InnerList.Remove(treasure);
                turnScope.PlayTreasure(treasure);
            }
        }

        public void RevealCard(Card card, IReactionScope externalEventScope, Player player)
        {
            card.Reveal(externalEventScope, player);
        }

        public void TrashCard(Card card, TrashPile trash, TurnScope turnScope)
        {
            MoveCardIntoSet(card, trash, turnScope);
        }

        private void MoveCardIntoSet(Card card, CardSet destination, TurnScope turnScope)
        {
            this.Remove(card);
            destination.Add(card, turnScope);
        }

        #region IHandleEvents
        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            this.OfType<IHandleExternalEvents>().ForEach(card => card.Handle(@event, scope));
        }

        public void Handle(IGameMessage @event, IActionScope scope)
        {
            this.ToList().OfType<IHandleInternalEvents>().ForEach(card => card.Handle(@event, scope));
        }
        #endregion
    }
}