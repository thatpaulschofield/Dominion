namespace Dominion.Cards.BasicSet.Treasures
{
    public class SilverFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(coins: 2, isTreasure: true, baseCost: 3, type: Treasure.Silver, name:"Silver");
        }
    }
}