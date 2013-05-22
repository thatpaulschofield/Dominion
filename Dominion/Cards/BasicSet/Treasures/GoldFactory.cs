namespace Dominion.Cards.BasicSet.Treasures
{
    public class GoldFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(coins: 3, isTreasure: true, cost: 5, type: Treasure.Gold);
        }
    }
}