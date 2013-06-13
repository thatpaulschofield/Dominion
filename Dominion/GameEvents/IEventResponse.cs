namespace Dominion.GameEvents
{
    public interface IEventResponse
    {
        string Description { get; }
        void Execute();
    }
}