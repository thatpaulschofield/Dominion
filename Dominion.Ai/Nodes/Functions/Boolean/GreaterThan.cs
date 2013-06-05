using System;
using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class GreaterThan : Function<bool, int, int>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) > Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "{1} > {2}";
        }
    }
}