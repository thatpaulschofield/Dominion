namespace Dominion.GameEvents
{
    public class GameCommand : GameMessage, ICommand
    {
        public GameCommand(ITurnScope turnScope) : base(turnScope)
        {
        }

        public IHandleEvents Recipient { get { return TurnScope.Player; } }
        public GameEventResponse Response { get; set; }
    }
}