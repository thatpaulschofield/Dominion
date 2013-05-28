using System;

namespace Dominion
{
    public class Money : IComparable<Money>
    {
        private readonly int _coins;

        public Money(int coins)
        {
            _coins = coins;
        }

        public int CompareTo(Money other)
        {
            return this._coins - other._coins;
        }

        public override string ToString()
        {
            return _coins.ToString();
        }

        public static implicit operator Int32(Money money)
        {
            return money._coins;
        }

        public static implicit operator Money(Int32 coins)
        {
            return new Money(coins);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Money;
            if (other == null)
                return false;
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _coins;
        }
    }
}