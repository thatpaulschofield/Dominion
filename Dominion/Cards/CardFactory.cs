namespace Dominion.Cards
{
    public class CardFactory<T> : ICardFactory where T : Card, new()
    {
        public Card Create()
        {
            return new T();
        }
    }
}