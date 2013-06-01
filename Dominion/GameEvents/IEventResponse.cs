namespace Dominion.GameEvents
{
    public interface IEventResponse// : IGameMessage
    {
        string Description { get; }
        void Execute();
    }
}