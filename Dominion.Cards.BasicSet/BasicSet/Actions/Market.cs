namespace Dominion.Cards.BasicSet.Actions
{
    public class Market : TypedCard<Market>
    {
        public Market()
            : base(isAction:true, cost: 5, name: "Market")
        {

        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(new TurnState(1, 1, 1));
            turnScope.Player.DrawIntoHand(1, turnScope);
        }
    }
}