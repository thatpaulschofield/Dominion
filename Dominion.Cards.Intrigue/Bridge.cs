using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion.Cards.Intrigue
{
    public class Bridge : TypedCard<Bridge>
    {
        public Bridge() : base(cost: 4, isAction: true)
        {
        }

        public class BridgePricingEffect : GameMessage, ICardCostModifierEffect
        {
            public BridgePricingEffect(IActionScope scope) : base(scope)
            {
            }

            public Money CalculateCostAdjustment(Card card, IActionScope scope)
            {
                return -1.Coins();
            }
        }
    }
}