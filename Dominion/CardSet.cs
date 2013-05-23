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

        public void Into(CardSet hand)
        {
            _innerList.ToList().ForEach(
                c =>
                    {
                        hand.Add(c);
                        _innerList.Remove(c);
                    });
        }

        public void DiscardInto(DiscardPile discardPile)
        {
            this.ToList().ForEach(card =>
                {
                    _innerList.Remove(card);
                    discardPile.Discard(card);
                });
        }

        public static implicit operator CardSet(Card card)
        {
            return new CardSet(card);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        public void Add(Card card)
        {
            _innerList.Add(card);
        }
    }
}