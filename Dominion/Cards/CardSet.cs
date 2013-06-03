using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Cards
{
    public class CardSet : IEnumerable<Card>
    {
        protected List<Card> InnerList = new List<Card>();

        public CardSet()
        {
            InnerList = new List<Card>();
        }

        public CardSet(IEnumerable<Card> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            InnerList = new List<Card>();
            InnerList.AddRange(cards);
        }

        public CardSet(params Card[] cards)
        {
            InnerList = new List<Card>();
            InnerList.AddRange(cards);
        }

        public CardSet Actions()
        {
            return new CardSet(this.Where(c => c.IsAction));
        }

        public CardSet Treasures()
        {
            return new CardSet(this.Where(c => c.IsTreasure));
        }

        public Card this[int index]
        {
            get { return InnerList[index]; }
        }

        public CardSet this[CardType type]
        {
            get { return new CardSet(this.Where(c => c.CardType == type)); }
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            InnerList.AddRange(cards);
        }

        public void Into(CardSet targetCardSet, IActionScope turnScope)
        {
            InnerList.ToList().ForEach(
                c =>
                    {
                        targetCardSet.Add(c, turnScope);
                        Remove(c);
                    });
        }

        public void DiscardInto(DiscardPile discardPile, IActionScope turnScope)
        {
            this.ToList().ForEach(card =>
                {
                    Remove(card);
                    discardPile.Discard(card, turnScope);
                });
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        public void Add(Card card, IActionScope turnScope)
        {
            if (card == null)
                return;

            InnerList.Add(card);
            OnCardAdded(card, turnScope);
        }

        protected virtual void OnCardAdded(Card card, IActionScope turnScope)
        {
            
        }

        public void Remove(Card card)
        {
            InnerList.Remove(card);
            OnCardRemoved(card);
        }

        protected virtual void OnCardRemoved(Card card)
        {
            
        }

        public static implicit operator CardSet(Card card)
        {
            return new CardSet(card);
        }

        public CardSet VictoryCards()
        {
            return new CardSet(this.Where(c => c.IsVictory));
        }

        public override string ToString()
        {
            return String.Format("Cards: [{0}]", this.Aggregate(String.Empty, (s, card) => s + " " + card.Name));
        }
    }
}