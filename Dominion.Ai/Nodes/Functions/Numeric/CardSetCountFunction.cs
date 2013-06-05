using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class CardSetCountFunction : Function<int, CardSet>
    {
        public override int Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).Count();
        }

        public override string ToString()
        {
            return "the number of cards in set {1}";
        }
    }
}