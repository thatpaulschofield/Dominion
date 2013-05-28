namespace Dominion.Cards.BasicSet.Actions
{
    public abstract class TypedCard<T> : Card where T: Card, new()
    {
        protected TypedCard(int coins = 0, bool isAction = false, bool isAttack = false, bool isTreasure = false, int victoryPoints = 0, bool isVictory = false, int cost = 0, string name = null) : 
            base(new CardType<T>(), coins, isAction, isAttack, isTreasure, victoryPoints, isVictory, cost, name)
        {
        }

        protected bool Equals(TypedCard<T> other)
        {
            return other.GetType() == this.GetType();
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == this.GetType();
        }

        public override int GetHashCode()
        {
            return typeof (T).GetHashCode();
        }
    }
}