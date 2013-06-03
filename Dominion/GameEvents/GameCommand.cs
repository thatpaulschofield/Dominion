namespace Dominion.GameEvents
{
    public class GameCommand : GameMessage, ICommand
    {
        public GameCommand(ITurnScope turnScope) : base(turnScope)
        {
            TurnScope = turnScope;
        }

        public ITurnScope TurnScope { get; private set; }
        public virtual void Execute(IPlayer player)
        {
            
        }
    }
}