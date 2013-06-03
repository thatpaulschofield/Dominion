using Dominion.AI;
using Dominion.Ai.Nodes.Terminals;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class CardTypeConstant : Constant<CardType>
    {
        public override CardType Evaluate(IAiContext context)
        {
            return Value;
        }
    }
}