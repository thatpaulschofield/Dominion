namespace Dominion.AI.Functions.Boolean
{
    public class Equals<TValue> : Function<bool, TValue, TValue>
    {
        public override bool Evaluate(TurnScope scope)
        {
            return Child1.Evaluate(scope).Equals(Child2.Evaluate(scope));
        }
    }
}
