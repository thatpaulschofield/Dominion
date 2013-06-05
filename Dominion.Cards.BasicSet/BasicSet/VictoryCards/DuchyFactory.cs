namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class DuchyFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Duchy, victoryPoints: 2, cost: 5, isVictory: true, name:"Duchy");
        }
    }
}