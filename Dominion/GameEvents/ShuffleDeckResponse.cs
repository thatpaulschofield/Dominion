namespace Dominion.GameEvents
{
    public class ShuffleDeckResponse : GameEventResponse<DeckDepletedEvent>
    {
        public ShuffleDeckResponse(ITurnScope turnScope) : base(turnScope)
        {
        }

        public override void Execute()
        {
            base.TurnScope.ActingPlayer.ShuffleDiscardPileIntoDeck(TurnScope);
        }
    }
}