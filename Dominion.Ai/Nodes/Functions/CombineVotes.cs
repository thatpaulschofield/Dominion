using Dominion.AI;
using StructureMap;

namespace Dominion.Ai.Nodes.Functions
{
    public class CombineVotes : Function<ResponseVotes,ResponseVotes,ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) + Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "combine two sets of votes";
        }
    }
}
