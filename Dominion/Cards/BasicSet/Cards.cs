namespace Dominion.Cards.BasicSet
{
    public class CurseFactory : ICardFactory
    {
        public Card Create()
        {
            return new Card(BasicCards.Curse, victoryPoints: -1);
        }
    }
}