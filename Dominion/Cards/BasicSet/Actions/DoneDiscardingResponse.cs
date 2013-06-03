using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class DoneDiscardingResponse : GameEventResponse
    {
        private int _discardedCount;

        public DoneDiscardingResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "None";
        }

        public override void Execute()
        {
            TurnScope.ActingPlayer.Draw(_discardedCount, TurnScope);
        }

        public void IncrementDiscardedCount()
        {
            _discardedCount++;
        }
    }
}