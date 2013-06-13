using System;
using Dominion.Ai.Nodes.Terminals;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class GameEventResponseConstant : Constant<EventResponseCriteria>
    {
        public override string ToString()
        {
            return String.Format ("the response [{0}]", Value == null ? "none" : Value.Name);
        }
    }
}