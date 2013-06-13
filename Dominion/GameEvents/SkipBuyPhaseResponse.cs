
namespace Dominion.GameEvents
{
    public class SkipBuyPhaseResponse : GameEventResponse<BuyPhase>
    {
        public SkipBuyPhaseResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "None";
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
        }

        public override string ToString()
        {
            return string.Format("{0}: no buy", TurnScope.Player.Name);
        }
    }
}