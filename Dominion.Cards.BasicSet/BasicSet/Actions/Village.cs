namespace Dominion.Cards.BasicSet.Actions
{
    public class Village : TypedCard<Village>
    {
        public Village()
            : base(isAction: true, cost: 3, name: "Village")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Player.DrawIntoHand(1, turnScope);
            turnScope.ChangeState(2.TurnActions());
        }
    }
}

