namespace Dominion.Cards.BasicSet.Actions
{
    public class Militia : TypedCard<Militia>
    {
        public Militia()
            : base(isAction: true, cost: 5, name: "Militia")
        {
        }
    }
}