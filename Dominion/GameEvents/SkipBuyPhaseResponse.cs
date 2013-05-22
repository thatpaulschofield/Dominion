using Dominion.AI;

namespace Dominion.GameEvents
{
    public class SkipBuyPhaseResponse : GameEventResponse
    {
        public SkipBuyPhaseResponse(TurnScope turnScope) : base(turnScope)
        {
        }

        public override void Execute()
        {
            
        }
    }
}