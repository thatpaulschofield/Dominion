namespace Dominion.Cards.BasicSet.VictoryCards
{
    public class ProvinceFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(Victory.Province, baseVictoryPoints:6, baseCost: 8, isVictory: true, name:"Province");
        }
    }
}