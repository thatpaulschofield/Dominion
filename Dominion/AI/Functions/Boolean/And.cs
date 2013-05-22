namespace Dominion.AI.Functions.Boolean
{
    public class And : Function<bool, bool, bool>
    {
        public override bool Evaluate(TurnScope scope)
        {
            return Child1.Evaluate(scope) && Child2.Evaluate(scope);
        }
    }
}