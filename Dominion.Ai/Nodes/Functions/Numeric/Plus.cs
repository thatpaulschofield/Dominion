using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class Plus : Function<int, int, int>
    {
        public override int Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) + Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "+";
        }
    }
}
