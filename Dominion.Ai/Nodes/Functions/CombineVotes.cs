using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion.AI.Functions
{
    public class CombineVotes : Function<ResponseVotes,ResponseVotes,ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) + Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "combine two sets of votes";
        }
    }
}
