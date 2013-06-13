using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class CountOfCardsInSetFunction : Function<int, CardSet>
    {
        public override int Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).Count();
        }

        public override string ToString()
        {
            return "count of cards in the set";
        }
    }
}