using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class CanRevealCardEvent : GameReactionMessage
    {
        private readonly Card _card;

        public CanRevealCardEvent(Card card, IReactionScope eventScope) : base(eventScope)
        {
            _card = card;
            _availableResponses.Add(new RevealCardResponse(card, eventScope));
            _availableResponses.Add(new DoNotRevealCardResponse(eventScope));
        }
    }
}