namespace Dominion.Cards.BasicSet.Actions
{
    public class Militia : TypedCard<Militia>
    {
        public Militia()
            : base(isAction: true, cost: 4, name: "Militia")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(2.TurnCoins());
            turnScope.Publish(new AttackCardPlayed(this, turnScope));
            turnScope.Publish(new MilitiaAttackEffect(turnScope));
        }
    }
}