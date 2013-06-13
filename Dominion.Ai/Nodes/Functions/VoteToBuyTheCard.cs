using System.Linq;
using Dominion.AI;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Functions
{
    public class VoteToBuyTheCard : Function<ResponseVotes, Card, int>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            var vote = context.AvailableResponses
                              .OfType<BuyCardResponse>()
                              .FirstOrDefault(response => (response.Item.Equals(Child1.Evaluate(context))));
            
            return vote == null 
                       ? new ResponseVotes() 
                       : new ResponseVotes().VoteFor(vote, Child2.Evaluate(context));
        }

        public override string ToString()
        {
            return "vote to buy the card {1} {2} times";
        }
    }
}