namespace Dominion.Cards.BasicSet.Actions
{
    public class Cellar : Card
    {
        public Cellar()
            : base(Action.Cellar, cost: 2, name: "Cellar")
        {

        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == this.GetType();
        }
    }
}