namespace Dominion.Cards.BasicSet.Actions
{
    public interface IFilterExternalGameEvents : IHandleEvents
    {
        IHandleEvents Next { get; set; }
    }
}