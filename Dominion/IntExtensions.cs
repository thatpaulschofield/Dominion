using System.Collections.Generic;
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
        
        public static CardSet Silvers(this int count)
        {
            return BuildSet(count, Treasure.Silver);
        }
        
        public static CardSet Golds(this int count)
        {
            return BuildSet(count, Treasure.Gold);
        }

        public static CardSet Estates(this int count)
        {
            return BuildSet(count, Victory.Estate);
        }
        
        public static CardSet Duchies(this int count)
        {
            return BuildSet(count, Victory.Duchy);
        }
        
        public static CardSet Provinces(this int count)
        {
            return BuildSet(count, Victory.Province);
        }
        
        public static CardSet Curses(this int count)
        {
            return BuildSet(count, BasicCards.Curse);
        }

        public static CardSet Villages(this int count)
        {
            return BuildSet(count, BasicCards.Actions.Village);
        }

        public static CardSet Cellars(this int count)
        {
            return BuildSet(count, BasicCards.Actions.Cellar);
        }


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