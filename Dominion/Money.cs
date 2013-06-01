using System;

namespace Dominion
{
    public struct Money : IComparable<Money>
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
            return String.Format("({0})", _coins);
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
            if (obj is Money)
            {
                var other = (Money) obj;
                return this.GetHashCode() == other.GetHashCode();
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return _coins;
        }
    }
}