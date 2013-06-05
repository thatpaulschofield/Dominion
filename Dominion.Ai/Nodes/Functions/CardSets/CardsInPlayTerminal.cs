using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class CardsInPlayTerminal : Terminal<CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(context.CardsInPlay ?? new CardSet());
        }

        public override string ToString()
        {
            return "cards currently in play";
        }
    }
}