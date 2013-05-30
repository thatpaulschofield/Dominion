namespace Dominion.GameEvents
{
    public abstract class GameReactionMessage : GameMessage, IEventResponse
    {
        protected readonly IReactionScope _externalEventScope;

        protected GameReactionMessage(IReactionScope externalEventScope) : base(externalEventScope)
        {
            _externalEventScope = externalEventScope;
        }

        public virtual void Execute(){}
    }
}