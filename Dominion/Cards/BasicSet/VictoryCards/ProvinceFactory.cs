namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class ProvinceFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Province, victoryPoints: 5, cost: 8, isVictory: true, name:"Province");
        }
    }
}