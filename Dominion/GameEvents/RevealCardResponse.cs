using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class DoNotRevealCardResponse : GameReactionMessage
    {
        public DoNotRevealCardResponse(IReactionScope externalEventScope) : base(externalEventScope)
        {
            Description = "Do not reveal card";
        }
    }

    public class RevealCardResponse : GameReactionMessage
    {
        private readonly Card _card;

        public RevealCardResponse(Card card, IReactionScope eventScope)
            : base(eventScope)
        {
            _card = card;
            Description = "Reveal " + card.Name;
        }

        public override void Execute()
        {
            _externalEventScope.ReceivingPlayer.RevealCard(_card, _externalEventScope);
        }

        public override string ToString()
        {
            return Description;
        }
    }

}