namespace Dominion.Cards.BasicSet.Actions
{
    public class Mine : TypedCard<Mine>
    {
        public Mine()
            : base(isAction: true, cost: 5, name: "Mine")
        {
        }
    }
}