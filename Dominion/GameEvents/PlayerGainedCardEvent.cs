namespace Dominion.GameEvents
{
    public class PlayerGainedCardEvent : GameMessage
    {
        public PlayerGainedCardEvent(ITurnScope turnScope) : base(turnScope)
        {
        }
    }
}