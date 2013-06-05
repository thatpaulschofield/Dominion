using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.CardSets
{
    public class CardsInHandTerminal : Terminal<CardSet>
    {
        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(context.Hand ?? new CardSet());
        }

        public override string ToString()
        {
            return "the set of cards in the player's hand";
        }
    }
}