using Dominion.AI;
using Dominion.Ai.Nodes.Terminals;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class ResponseVotesConstant : Constant<ResponseVotes>
    {
        public override string ToString()
        {
            return "zero votes";
        }
    }

    public class CardTypeConstant : Constant<CardType>
    {
        public override CardType Evaluate(IAiContext context)
        {
            return Value;
        }

        public override string ToString()
        {
            return "the card type [" + ((Value == null) ? "N/A" : Value.ToString()) + "]";
        }
    }
}