namespace Dominion.Cards.BasicSet.Treasures
{
    public class GoldFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(coins: 3, isTreasure: true, baseCost: 6, type: Treasure.Gold, name: "Gold");
        }
    }
}