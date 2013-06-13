using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet.Actions;

namespace Dominion.Cards.BasicSet.BasicSet.VictoryCards
{
    public class Gardens : TypedCard<Gardens>
    {
        public Gardens() :  base(isVictory:true, cost:4)
        {
            
        }

        public override int Score(ITurnScope scope)
        {
            return scope.Deck.Count()/10;
        }
    }
}
