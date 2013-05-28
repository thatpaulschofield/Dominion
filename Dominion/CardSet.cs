using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class CardSet : IEnumerable<Card>
    {
        protected List<Card> _innerList;

        public CardSet()
        {
            _innerList = new List<Card>();
        }

        public CardSet(IEnumerable<Card> cards) : this()
        {
            _innerList.AddRange(cards);
        }

        public CardSet(params Card[] cards) : this(cards.ToList())
        {
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
            get { return _innerList[index]; }
        }

        public CardSet this[CardType type]
        {
            get { return new CardSet(this.Where(c => c.CardType == type)); }
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            _innerList.AddRange(cards);
        }

        public void Into(CardSet targetCardSet, ITurnScope turnScope)
        {
            _innerList.ToList().ForEach(
                c =>
                    {
                        targetCardSet.Add(c, turnScope);
                        Remove(c);
                    });
        }

        public void DiscardInto(DiscardPile discardPile, ITurnScope turnScope)
        {
            this.ToList().ForEach(card =>
                {
                    Remove(card);
                    discardPile.Discard(card, turnScope);
                });
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        public void Add(Card card, ITurnScope turnScope)
        {
            if (card == null)
                return;

            _innerList.Add(card);
            OnCardAdded(card, turnScope);
        }

        protected virtual void OnCardAdded(Card card, ITurnScope turnScope)
        {
            
        }

        public void Remove(Card card)
        {
            _innerList.Remove(card);
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