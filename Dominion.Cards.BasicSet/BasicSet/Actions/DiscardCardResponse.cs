using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class DiscardCardReaction : GameEventReaction
    {
        private readonly Card _card;
        public DiscardCardReaction(IReactionScope externalEventScope, Card card) : base(externalEventScope)
        {
            _card = card;
            Description = "Discard " + _card.Name;
        }

        public override void Execute()
        {
            
            TurnScope.Discard(_card);
        }
    }
}