using Dominion.Cards.BasicSet.Actions;

namespace Dominion.Cards.BasicSet
{
    public class BasicCards
    {
        public static CardType Curse = new CardType(new CurseFactory());


        public static CardType Card<T>() where T : Card, new()
        {
            return new CardType<T>();
        }

        public class Actions
        {
            public static CardType Village = new CardType(new CardFactory<Village>());

            public static CardType Cellar = new CardType(new CardFactory<Cellar>());

        }

        
    }
}