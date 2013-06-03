﻿using System.Linq;
using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Terminals
{
    public class VoteForMostExpensiveTreasure : Function<ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            var buyTreasureResponses =
                context.AvailableResponses.OfType<BuyCardResponse>().OrderByDescending(r => r.CardToPurchase.Coins);
            if (buyTreasureResponses.Any())
                return context.VoteFor(buyTreasureResponses.First(), 1);

            return context.Votes;
        }

        public override INode this[int i]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}