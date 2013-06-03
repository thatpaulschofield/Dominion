namespace Dominion.Cards.BasicSet.Actions
{
    public class Smithy : TypedCard<Smithy>
    {
        public Smithy()
            : base(isAction: true, cost: 3, name: "Smithy")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ActingPlayer.Draw(3, turnScope);
        }
    }
}