namespace Dominion.GameEvents
{
    public interface ICommand : IGameMessage
    {
        IHandleInternalEvents Recipient { get; }
        ITurnScope TurnScope { get; }
    }
}