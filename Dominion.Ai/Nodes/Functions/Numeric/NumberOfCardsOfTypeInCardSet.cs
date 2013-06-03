using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class NumberOfCardsOfTypeInCardSet : Function<int, CardType, CardSet>
    {
        public override int Evaluate(IAiContext context)
        {
            return Child2.Evaluate(context).Count(c => Equals(c.CardType, Child1.Evaluate(context)));
        }
    }
}