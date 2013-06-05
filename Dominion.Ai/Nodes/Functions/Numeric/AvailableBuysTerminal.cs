using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class AvailableBuysTerminal : Terminal<int>
    {
        public override int Evaluate(IAiContext context)
        {
            return context.Buys;
        }

        public override string ToString()
        {
            return "the remaining buys";
        }
    }
}