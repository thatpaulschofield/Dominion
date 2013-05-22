using Dominion.AI;

namespace Dominion.GameEvents
{
    public class SkipActionPhaseResponse : GameEventResponse
    {
        public SkipActionPhaseResponse(TurnScope turnScope) : base(turnScope)
        {
        }

        public override void Execute()
        {
        }
    }
}