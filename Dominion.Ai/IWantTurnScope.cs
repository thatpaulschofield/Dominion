namespace Dominion.AI
{
    public interface IWantTurnScope
    {
        ITurnScope TurnScope { get; set; }
    }
}