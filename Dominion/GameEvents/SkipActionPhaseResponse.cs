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
            TurnScope.ChangeState((-TurnScope.Actions).TurnActions());
        }

        public override string ToString()
        {
            return String.Format("Player {0} intends to skip action phase.", base.TurnScope.Player.Name);
        }
    }
}