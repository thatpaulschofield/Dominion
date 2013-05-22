namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class DuchyFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Estate, victoryPoints: 2, cost: 5);
        }
    }
}