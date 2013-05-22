namespace Dominion.Cards.BasicSet.Treasures
{
    public class Treasure
    {
        public static CardType Copper = new CardType(new CopperFactory());

        public static CardType Silver = new CardType(new SilverFactory());

        public static CardType Gold = new CardType(new GoldFactory());
    }
}