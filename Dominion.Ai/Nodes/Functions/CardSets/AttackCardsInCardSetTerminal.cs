using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class AttackCardsInCardSetTerminal : Terminal<CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(context.Hand.Where(c => c.IsAttack));
        }

        public override string ToString()
        {
            return "the set of attack cards in the card set";
        }
    }
}