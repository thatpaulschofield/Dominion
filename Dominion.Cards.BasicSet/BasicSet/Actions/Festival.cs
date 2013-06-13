namespace Dominion.Cards.BasicSet.Actions
{
    public class Festival : TypedCard<Festival>
    {
        public Festival() : base(cost: 5, isAction: true)
        {
            
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(new TurnState(actions:2, buys:1, coins:2));
        }
    }
}