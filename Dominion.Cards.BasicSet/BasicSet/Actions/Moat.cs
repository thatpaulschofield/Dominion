using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Moat : TypedCard<Moat>, IHandleExternalEvents
    {
        public Moat()
            : base(isAction: true, isAttack: true, cost: 2, name: "Moat")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ActingPlayer.Draw(2, turnScope);
        }

        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            if (@event is AttackCardPlayed)
            {
                
                @event.ActionScope.Publish(new CanRevealCardEvent(this, scope));
            }
        }

        private ExternalEventFilter MoatEffect()
        {
            return new ExternalEventFilter((message) => !(message is IAttackEffect));
        }

        public override void OnRevealed(IReactionScope externalEventScope, Player player)
        {
            externalEventScope.RegisterEventFilter(MoatEffect());
        }
    }
}