
namespace Dominion.GameEvents
{
    public class SkipBuyPhaseResponse : GameEventResponse<BuyPhase>
    {
        public SkipBuyPhaseResponse(ITurnScope turnScope) : base(turnScope)
        {
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
        }
    }
}