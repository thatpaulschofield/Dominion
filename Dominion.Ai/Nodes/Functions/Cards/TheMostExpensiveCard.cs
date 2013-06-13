using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.AI;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Treasures;

namespace Dominion.Ai.Nodes.Functions.Cards
{
    public class TheMostExpensiveCard : Function<Card, CardSet>
    {
        public override Card Evaluate(IAiContext context)
        {
            var cardSet = Child1.Evaluate(context);

            return cardSet
                .OrderByDescending(c => c.BaseCost).FirstOrDefault();
        }

        public override string ToString()
        {
            return "the most expensive card in the set";
        }
    }
}
