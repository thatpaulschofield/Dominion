using System;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using Action = System.Action;

namespace Dominion
{
    public static class IntExtensions
    {
        public static CardSet Coppers(this int count)
        {
            return BuildSet(count, Treasure.Copper);
        }

        public static CardSet Estates(this int count)
        {
            return BuildSet(count, Victory.Estate);
        }

        public static CardSet BuildSet(int count, CardType card)
        {
            var cards = new CardSet();
            for (int i = 0; i < count; i++)
            {
                cards.Add(card.Create());
            }
            return cards;
        }

        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}