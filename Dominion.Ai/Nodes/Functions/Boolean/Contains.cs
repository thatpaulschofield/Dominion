using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Boolean
{
    public class Contains : Function<bool, CardSet, Card>
    {
        public override bool Evaluate(IAiContext context)
        {
            var cardSet = Child1.Evaluate(context);
            var card = Child2.Evaluate(context);
            if (cardSet == null || card == null)
                return false;

            return cardSet.Contains(card);
        }

        public override string ToString()
        {
            return "the cardset {1} contains the card {2}";
        }
    }
}