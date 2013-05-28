using System;
using Dominion.AI;

namespace Dominion.GameEvents
{
    public class SkipActionPhaseResponse : GameEventResponse
    {
        public SkipActionPhaseResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "None";
        }

        public override void Execute()
        {
            TurnScope.ChangeState(new TurnState(-TurnScope.Actions, 0, 0));
        }

        public override string ToString()
        {
            return String.Format("Player {0} intends to skip action phase.", base.TurnScope.Player.Name);
        }
    }
}