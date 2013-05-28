using System;

namespace Dominion.Cards
{
    public class CardFactory<T> : ICardFactory where T : Card, new()
    {
        public Card Create()
        {
            return new T();
        }

        public override int GetHashCode()
        {
            return Create().GetHashCode();
        }
    }

    public class CardFactory : ICardFactory
    {
        private readonly Type _type;

        public CardFactory(Type type)
        {
            _type = type;
        }

        public Card Create()
        {
            return Activator.CreateInstance(_type) as Card;
        }

        public override int GetHashCode()
        {
            return Create().GetHashCode();
        }
    }
}