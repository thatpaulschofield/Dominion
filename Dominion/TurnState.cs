using System;

namespace Dominion
{
    public class TurnState
    {
        private readonly int _actions;
        private readonly int _buys;
        private readonly Money _coins;

        public TurnState(int actions, int buys, int coins)
        {
            _actions = actions;
            _buys = buys;
            _coins = coins;
        }

        public int Actions { get { return _actions; } }
        public int Buys { get { return _buys; } }
        public Money Coins { get { return _coins; } }

        public TurnState AddActions(int actions)
        {
            return new TurnState(_actions + actions, _buys, _coins);
        }

        public TurnState AddBuys(int buys)
        {
            return new TurnState(_actions, _buys + buys, _coins);
        }

        public TurnState AddCoins(int coins)
        {
            return new TurnState(_actions, _buys, _coins + coins);
        }

        public TurnState RegisterBuy(int cost)
        {
            return new TurnState(_actions, _buys - 1, _coins - cost);
        }

        public static TurnState operator +(TurnState turnState, Money money)
        {
            return new TurnState(turnState._actions, turnState._buys, turnState._coins + money);
        }

        public static TurnState operator +(TurnState turnState1, TurnState turnState2)
        {
            return new TurnState(turnState1._actions + turnState2._actions, turnState1._buys + turnState2._buys, turnState1._coins + turnState2._coins);
        }


        public override string ToString()
        {
            return String.Format("{0} Actions, {1} Buys, {2} Coins", Actions, Buys, Coins);
        }


        #region Equality
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TurnState) obj);
        }

        protected bool Equals(TurnState other)
        {
            return _actions == other._actions && _buys == other._buys && Equals(_coins, other._coins);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _actions;
                hashCode = (hashCode * 397) ^ _buys;
                hashCode = (hashCode * 397) ^ _coins;
                return hashCode;
            }
        }
        #endregion

        public static TurnState NewTurn()
        {
            return new TurnState(1, 1, 0);
        }
    }
}