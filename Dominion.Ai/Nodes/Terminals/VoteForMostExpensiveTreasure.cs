using System.Linq;
using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Terminals
{
    public class VoteForMostExpensiveTreasure : Terminal<ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            var buyTreasureResponses =
                context.AvailableResponses.OfType<BuyCardResponse>().OrderByDescending(r => r.Item.Coins);
            if (buyTreasureResponses.Any())
                return context.VoteFor(buyTreasureResponses.First(), 1);

            return context.Votes;
        }

        public override string ToString()
        {
            return "vote to buy the most expensive treasure I can";
        }
    }
}