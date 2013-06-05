using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Cards
{
    public class TheMostExpensiveCard : Function<Card, CardSet>
    {
        public override Card Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).OrderByDescending(c => c.Cost).FirstOrDefault();
        }

        public override string ToString()
        {
            return "the most expensive card in {1}";
        }
    }
}
