using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class CardsOfTypeFunction : Function<CardSet, CardSet, CardType>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(Child1.Evaluate(context).Where(c => c.CardType.Equals(Child2.Evaluate(context))));
        }

        public override string ToString()
        {
            return "cards in set {1} of type {2}";
        }
    }
}