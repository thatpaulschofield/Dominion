namespace Dominion.AI.Functions.Boolean
{
    public class And : Function<bool, bool, bool>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) && Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "and";
        }
    }
}