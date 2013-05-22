namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class EstateFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Estate, victoryPoints: 1, cost: 2);
        }
    }
}