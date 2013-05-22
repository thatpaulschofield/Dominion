namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class Victory
    {
        public static CardType Estate = new CardType(new EstateFactory());

        public static CardType Duchy = new CardType(new DuchyFactory());

        public static CardType Province = new CardType(new ProvinceFactory());
    }
}