using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class Contains : Function<bool, CardSet, Card>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).Contains(Child2.Evaluate(context));
        }

        public override string ToString()
        {
            return "the cardset {1} contains the card {2}";
        }
    }
}