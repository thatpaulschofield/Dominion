using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class ActionsInCardSetFunction : Function<CardSet, CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(Child1.Evaluate(context).Where(c => c.IsAction));
        }

        public override string ToString()
        {
            return "action cards in the card set";
        }
    }
}