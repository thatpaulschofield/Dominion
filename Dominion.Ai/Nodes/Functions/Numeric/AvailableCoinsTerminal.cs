using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class AvailableCoinsTerminal : Terminal<int>
    {
        public override int Evaluate(IAiContext context)
        {
            return context.Coins;
        }

        public override string ToString()
        {
            return "the remaining coins";
        }
    }
}