namespace Dominion.GameEvents
{
    public class GameCommand : GameMessage, ICommand
    {
        public GameCommand(IActionScope scope)
            : base(scope)
        {
            TurnScope = scope.GetTurnScope;
        }

        public ITurnScope TurnScope { get; private set; }
        public virtual void Execute(IPlayer player)
        {
            
        }
    }
}