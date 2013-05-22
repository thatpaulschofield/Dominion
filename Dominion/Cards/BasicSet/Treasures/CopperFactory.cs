namespace Dominion.Cards.BasicSet.Treasures
{
    public class CopperFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(coins: 1, isTreasure: true, type: Treasure.Copper);
        }
    }
}