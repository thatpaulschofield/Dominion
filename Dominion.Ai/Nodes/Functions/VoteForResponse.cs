using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Functions
{
    public class VoteForResponseFunction : Function<ResponseVotes, GameEventResponse, int, bool>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            var votes = new ResponseVotes();

            if (Child3.Evaluate(context) && context.ResponseIsAvailable(Response(context)))
                votes.VoteFor(Response(context), Child2.Evaluate(context));

            return votes;
        }

        protected GameEventResponse Response(IAiContext context)
        {
            return Child1.Evaluate(context); 
        }

        public override string ToString()
        {
            return "vote for the {1} response {2} times if {3}";
        }
    }
}