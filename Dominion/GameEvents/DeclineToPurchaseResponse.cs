namespace Dominion.GameEvents
{
    public class DeclineToPurchaseResponse : GameEventResponse<BuyPhase>
    {
        public DeclineToPurchaseResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "End turn";
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
            TurnScope.ChangeState((-TurnScope.Buys).TurnBuys());
        }
    }
}