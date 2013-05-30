using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class DiscardCardResponse : GameReactionMessage
    {
        private readonly Card _card;
        public DiscardCardResponse(IReactionScope externalEventScope, Card card) : base(externalEventScope)
        {
            _card = card;
            Description = "Discard " + _card.Name;
        }

        public override void Execute()
        {
            _externalEventScope.ReceivingPlayer.Discard(_card, _externalEventScope);
        }
    }
}