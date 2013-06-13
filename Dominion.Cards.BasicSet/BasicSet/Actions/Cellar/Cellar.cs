namespace Dominion.Cards.BasicSet.Actions
{
    public class Cellar : TypedCard<Cellar>
    {
        public Cellar()
            : base(isAction: true, cost: 2, name: "Cellar")
        {

        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(1.TurnActions());
            turnScope.Player.Handle(new OptionallyDiscard(turnScope), turnScope);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj.GetType() == this.GetType();
        }
    }
}