namespace Dominion.Cards.BasicSet.Actions
{
    public class Moat : TypedCard<Moat>
    {
        public Moat()
            : base(isAction: true, isAttack: true, cost: 2, name: "Moat")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Player.Draw(2, turnScope);
        }
    }
}