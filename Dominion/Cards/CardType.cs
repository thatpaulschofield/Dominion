using System;

namespace Dominion.Cards
{
    public class CardType<T> : CardType where T : Card, new()
    {
        public CardType() : base(new CardFactory<T>())
        {
            
        }

        protected bool Equals(CardType<T> other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return Create().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as CardType;
            if (other == null)
                return false;

            return this.Create().Equals(other.Create());
        }
    }

    public class CardType
    {
        private readonly ICardFactory _cardFactory;

        public CardType(Type cardType)
        {
            _cardFactory = new CardFactory(cardType);
        }

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

        public override bool Equals(object obj)
        {
            var other = obj as CardType;
            if (other == null)
                return false;

            return this.Create().Equals(other.Create());
        }

        public override int GetHashCode()
        {
            return (_cardFactory != null ? _cardFactory.GetHashCode() : 0);
        }

        protected bool Equals(CardType other)
        {
            return Equals(_cardFactory, other._cardFactory);
        }

        public override string ToString()
        {
            return Create().Name;
        }
    }
}