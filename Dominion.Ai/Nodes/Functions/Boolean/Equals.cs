using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class Equals<TValue> : Function<bool, TValue, TValue>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).Equals(Child2.Evaluate(context));
        }

        public override string ToString()
        {
            return "== (" + typeof(TValue).Name + " value)";
        }
    }
}
