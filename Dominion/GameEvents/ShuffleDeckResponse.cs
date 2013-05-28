namespace Dominion.GameEvents
{
    public class ShuffleDeckResponse : GameEventResponse
    {
        public ShuffleDeckResponse(ITurnScope turnScope) : base(turnScope)
        {
        }

        public override void Execute()
        {
            base.TurnScope.Player.ShuffleDiscardPileIntoDeck(TurnScope);
        }
    }
}