namespace Dominion.GameEvents
{
    public class GameCommand : GameMessage, ICommand
    {
        public GameCommand(ITurnScope turnScope) : base(turnScope)
        {
            TurnScope = turnScope;
        }

        public ITurnScope TurnScope { get; private set; }
        public IHandleInternalEvents Recipient { get { return TurnScope.Player; } }
        public GameEventResponse Response { get; set; }
    }
}