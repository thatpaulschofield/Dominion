namespace Dominion.GameEvents
{
    public class ShuffleDeckResponse : GameEventResponse<DeckDepletedEvent>
    {
        public ShuffleDeckResponse(IActionScope scope) : base(scope)
        {
        }

        public override void Execute()
        {
            ActionScope.Player.ShuffleDiscardPileIntoDeck(ActionScope);
        }
    }
}