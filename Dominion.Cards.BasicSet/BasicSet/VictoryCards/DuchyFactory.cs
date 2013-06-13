namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class DuchyFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Duchy, baseVictoryPoints:3, baseCost: 5, isVictory: true, name:"Duchy");
        }
    }
}