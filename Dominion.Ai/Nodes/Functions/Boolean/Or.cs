using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class Or : Function<bool, bool, bool>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) || Child2.Evaluate(context);
        }
    }
}