namespace Dominion.Cards
{
    public interface ICardFactory
    {
        Card Create();
    }

    public class CardType
    {
        private readonly ICardFactory _cardFactory;

        public CardType(ICardFactory cardFactory)
        {
            _cardFactory = cardFactory;
        }

        public Card Create()
        {
            return _cardFactory.Create();
        }

        public static implicit operator Card(CardType type)
        {
            return type.Create();
        }
    }
}