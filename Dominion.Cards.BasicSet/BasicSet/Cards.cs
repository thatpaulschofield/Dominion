namespace Dominion.Cards.BasicSet
{
    public class CurseFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(BasicCards.Curse, baseVictoryPoints: -1, name:"Curse");
        }
    }
}