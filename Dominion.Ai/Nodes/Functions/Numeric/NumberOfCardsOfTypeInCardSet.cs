using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class NumberOfCardsOfTypeInCardSet : Function<int, CardSet, CardType>
    {
        public override int Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context)
                .Count(c => Equals(c.CardType, Child2.Evaluate(context)));
        }

        public override string ToString()
        {
            return "the number of cards of this type in the set";
        }
    }
}