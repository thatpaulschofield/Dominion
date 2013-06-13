namespace Dominion.Cards.BasicSet.Actions
{
    public class Laboratory : TypedCard<Laboratory>
    {
        public Laboratory() : base(cost:5, isAction:true, name:"Laboratory")
        {}
        
        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Player.DrawIntoHand(2, turnScope);
            turnScope.ChangeState(1.TurnActions());
        }

    }
}