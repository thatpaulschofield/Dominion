namespace Dominion.Cards.BasicSet.Actions
{
    public class Woodcutter : TypedCard<Woodcutter>
    {
        public Woodcutter()
            : base(isAction: true, cost: 3, name: "Woodcutter")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(1.TurnBuys(), 2.TurnCoins());
        }
    }
}