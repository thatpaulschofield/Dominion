using Dominion.AI;

namespace Dominion.GameEvents
{
    public abstract class GameEventResponse : GameMessage
    {
        protected readonly ITurnScope TurnScope;

        protected GameEventResponse(ITurnScope turnScope) : base(turnScope)
        {
            TurnScope = turnScope;
        }

        public string Description { get; set; }

        public abstract void Execute();
    }
}