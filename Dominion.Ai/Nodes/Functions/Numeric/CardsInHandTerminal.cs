using System.Collections.Generic;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class CardsInHandTerminal : Terminal<CardSet>, IWantToViewPlayerHand
    {
        private CardSet _hand;

        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(_hand ?? new CardSet());
        }

        public void ShowPlayerHand(CardSet hand)
        {
            _hand = hand;
        }
    }
}