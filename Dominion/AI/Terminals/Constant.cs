using Dominion.Cards;

namespace Dominion.AI.Terminals
{

    public class CardsAvailableForPurchase : Terminal<CardSet>
    {
        public override CardSet Evaluate(TurnScope scope)
        {
            return base.Evaluate(scope);
        }
    }

    public class Constant<T> : Terminal<T>
    {
        public Constant()
        {
            
        } 

        public Constant(T value)
        {
            Value = value;
        }

        public override T Evaluate(TurnScope scope)
        {
            return Value;
        }

        public T Value { get; set; }
    }
}
