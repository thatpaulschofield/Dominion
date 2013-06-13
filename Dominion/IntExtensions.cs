using System;
using System.Collections.Generic;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Action = System.Action;

namespace Dominion
{
    public static class IntExtensions
    {
        public static CardSet BuildSet(int count, CardType card)
        {
            return new CardSet(count.Of(card));
        }

        public static IEnumerable<Card> Of(this int count, CardType card)
        {
            for (int i = 0; i < count; i++)
            {
                yield return card.Create();
            }
        }

        public static IEnumerable<Card> Of<TCARDTYPE>(this int count) where TCARDTYPE: Card, new()
        {
            return Of(count, new CardType<TCARDTYPE>());
        }

        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public static T[] Times<T>(this int count, Func<T> action)
        {
            var items = new List<T>();
            for (int i = 0; i < count; i++)
            {
                items.Add(action());
            }
            return items.ToArray();
        }

        public static Money Coins(this int coins)
        {
            return new Money(coins);
        }

        public static TurnState TurnActions(this int actions)
        {
            return new TurnState(actions, 0, 0);
        }

        public static TurnState TurnBuys(this int buys)
        {
            return new TurnState(0, buys, 0);
        }

        public static TurnState TurnCoins(this int coins)
        {
            return new TurnState(0, 0, coins);
        }
    }
}