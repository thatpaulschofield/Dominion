using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.AI;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class NumberOfPlayersTerminal : Terminal<int>
    {
        public override int Evaluate(IAiContext context)
        {
            return context.Game.NumberOfPlayers;
        }

        public override string ToString()
        {
            return "the number of players";
        }
    }
}
