namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class GainCardResponse : GameEventResponse<Card>
    {
        public GainCardResponse(Card card, ITurnScope turnScope) : base(card, turnScope)
        {
            Description = "Gain a " + card.Name;
        }

        public override void Execute()
        {
            TurnScope.GainCardFromSupply(Item);
        }
    }
}