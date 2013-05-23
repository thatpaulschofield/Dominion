using System;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class Deck : CardSet
    {
        public Deck()
        {
            
        }

        public Deck(params CardSet[] cardSets)
        {
            cardSets.ToList().ForEach(_innerList.AddRange);
        }

        public bool CanDraw
        {
            get { return this.ToList().Any(); }
        }

        public Card Draw()
        {
            var drawn = _innerList[0];
            _innerList.RemoveAt(0);
            return drawn;
        }

        public Deck Shuffle()
        {
            var shuffled = new Deck(this);
            for (int i = 0; i < 100; i++)
            {
                int posA = new Random((int)DateTime.Now.Ticks).Next(shuffled._innerList.Count);
                int posB = new Random((int)DateTime.Now.Ticks).Next(shuffled._innerList.Count);
                Card temp = shuffled._innerList[posA];
                shuffled._innerList[posA] = shuffled._innerList[posB];
                shuffled._innerList[posB] = temp;
            }
            return shuffled;
        }

        public CardSet Draw(int cardCount)
        {
            var cardSet = new CardSet();
            cardCount.Times(() => cardSet.Add(this.Draw()));
            return cardSet;
        }
    }
}