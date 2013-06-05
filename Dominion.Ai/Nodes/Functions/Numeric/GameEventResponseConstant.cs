using System;
using Dominion.Ai.Nodes.Terminals;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class GameEventResponseConstant : Constant<GameEventResponse>
    {
        public override string ToString()
        {
            return String.Format ("the response [{0}]", Value.Description);
        }
    }
}