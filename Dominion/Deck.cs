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
            cardSets.ToList().ForEach(AddRange);
        }

        public bool CanDraw
        {
            get { return Count > 0; }
        }

        public Card Draw()
        {
            var drawn = this[0];
            this.RemoveAt(0);
            return drawn;
        }

        public Deck Shuffle()
        {
            var shuffled = new Deck(this);
            for (int i = 0; i < 100; i++)
            {
                int posA = new Random((int)DateTime.Now.Ticks).Next(shuffled.Count);
                int posB = new Random((int)DateTime.Now.Ticks).Next(shuffled.Count);
                Card temp = shuffled[posA];
                shuffled[posA] = shuffled[posB];
                shuffled[posB] = temp;
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