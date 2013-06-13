using System.Linq;
using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Functions
{
    public class VoteForResponseFunction<TINRESPONSETO, TRESPONSE, TITEM> 
        : Function<ResponseVotes, EventResponseWithItemCriteria<TINRESPONSETO, TRESPONSE, TITEM>, int, bool> 
            where TRESPONSE : GameEventResponseWithItem<TINRESPONSETO, TITEM>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            var votes = new ResponseVotes();

            var matchingResponses = context.AvailableResponses.OfType<TRESPONSE>().Where(r => Child1.Evaluate(context).IsMatch(r));
            matchingResponses.ForEach(response => context.VoteFor(response, Child2.Evaluate(context)));
            return votes;
        }

        public override string ToString()
        {
            return "vote for the {1} response {2} times if {3}";
        }
    }
}// EventResponseCriteria<TINRESPONSETO, TRESPONSE, TITEM>