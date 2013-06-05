using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class AvailableActionsTerminal : Terminal<int>
    {
        public override int Evaluate(IAiContext context)
        {
            return context.Actions;
        }

        public override string ToString()
        {
            return "the remaining actions";
        }
    }
}