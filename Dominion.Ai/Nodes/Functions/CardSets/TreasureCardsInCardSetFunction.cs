using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class TreasureCardsInCardSetFunction : Function<CardSet, CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(Child1.Evaluate(context).Where(c => c.IsTreasure));
        }

        public override string ToString()
        {
            return "the set of treasure cards in the card set";
        }
    }
}