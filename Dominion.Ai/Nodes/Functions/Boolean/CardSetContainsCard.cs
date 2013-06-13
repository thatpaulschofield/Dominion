using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class CardSetIsSupersetFunction : Function<bool, CardSet, CardSet>
    {
        public override bool Evaluate(IAiContext context)
        {
            if (Child1 == null || Child2 == null)
                return false;

            var cardSet1 = Child1.Evaluate(context);
            var cardSet2 = Child2.Evaluate(context);
            if (cardSet1 == null || cardSet2 == null)
                return false;

            return Child1.Evaluate(context).IsSupersetOf(Child2.Evaluate(context));
        }

        public override string ToString()
        {
            return "the set {1} contains all the cards in set {2}";
        }
    }

    public class CardSetContainsCardFunction : Function<bool, CardSet, Card>
    {
        public override bool Evaluate(IAiContext context)
        {
            if (Child1 == null || Child2 == null)
                return false;

            var cardSet = Child1.Evaluate(context);
            var card = Child2.Evaluate(context);
            if (cardSet == null || card == null)
                return false;

            return (cardSet.Contains(card));
        }

        public override string ToString()
        {
            return "the set {1} contains the card {2}";
        }
    }
}