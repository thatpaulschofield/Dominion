using Dominion.Cards.BasicSet.Actions;

namespace Dominion.Cards.BasicSet
{
    public class Action
    {
        public static CardType Village = new CardType(new CardFactory<Village>());
        public static CardType Cellar = new CardType(new CardFactory<Cellar>());
    }
}