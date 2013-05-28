namespace Dominion.GameEvents
{
    public interface ICommand : IMessage
    {
        IHandleEvents Recipient { get; }
    }
}