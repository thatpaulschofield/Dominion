namespace Dominion.AI.Functions
{
    public class Not : Function<bool, bool>
    {
        public override bool Evaluate(TurnScope scope)
        {
            return !Child1.Evaluate(scope);
        }
    }
}