namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class EstateFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Estate, baseVictoryPoints:2, baseCost: 2, isVictory: true, name:"Estate");
        }
    }
}