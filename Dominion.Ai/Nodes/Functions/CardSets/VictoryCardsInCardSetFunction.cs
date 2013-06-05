using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class VictoryCardsInCardSetFunction : Function<CardSet, CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(Child1.Evaluate(context).Where(c => c.IsVictory));
        }

        public override string ToString()
        {
            return "the set of victory cards in card set";
        }

    }
}