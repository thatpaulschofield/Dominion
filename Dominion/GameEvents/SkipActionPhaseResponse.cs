using System;

namespace Dominion.GameEvents
{
    public class SkipActionPhaseResponse : GameEventResponse<ActionPhase>
    {
        public SkipActionPhaseResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "None";
        }

        public override void Execute()
        {
            TurnScope.ChangeState((-TurnScope.Actions).TurnActions());
            TurnScope.Publish(this);
        }

        public override string ToString()
        {
            return String.Format("{0} intends to skip action phase.", base.TurnScope.ActingPlayer.Name);
        }
    }
}