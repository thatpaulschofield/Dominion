using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class CardDrawnEvent : GameMessage
    {
        private readonly Card _card;

        public CardDrawnEvent(Card card, IActionScope scope)
            : base(scope)
        {
            _card = card;
        }

        public override string ToString()
        {
            return string.Format("{0} drew a {1}", ActionScope.Player.Name, _card.Name);
        }
    }
}