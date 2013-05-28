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
            turnScope.ChangeState(1.TurnActions());
            turnScope.Player.Draw(2, turnScope);
        }
    }
}

