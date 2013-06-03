namespace Dominion.GameEvents
{
    public interface ICommand : IGameMessage
    {
        ITurnScope TurnScope { get; }
        void Execute(IPlayer player);
    }
}