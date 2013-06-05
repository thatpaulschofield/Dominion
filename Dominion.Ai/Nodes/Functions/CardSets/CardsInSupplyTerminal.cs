using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class CardsInSupplyTerminal : Terminal<CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(context.Supply ?? new CardSet());
        }

        public override string ToString()
        {
            return "cards in the supply";
        }
    }
}