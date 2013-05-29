namespace Dominion.Cards.BasicSet.Actions
{
    public class Militia : TypedCard<Militia>
    {
        public Militia()
            : base(isAction: true, cost: 5, name: "Militia")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new AttackCardPlayed(this, turnScope));
        }
    }
}