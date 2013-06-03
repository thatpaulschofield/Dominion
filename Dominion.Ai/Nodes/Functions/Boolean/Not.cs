using Dominion.GameEvents;

namespace Dominion.AI.Functions.Boolean
{
    public class Not : Function<bool, bool>
    {
        public override bool Evaluate(IAiContext context)
        {
            return !Child1.Evaluate(context);
        }

        public override string ToString()
        {
            return "not";
        }
    }
}