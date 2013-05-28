using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class CardDrawnEvent : GameMessage
    {
        private readonly Card _card;

        public CardDrawnEvent(Card card, ITurnScope scope) : base(scope)
        {
            _card = card;
        }

        public override string ToString()
        {
            return string.Format("Player {0} drew a {1}", TurnScope.Player.Name, _card.Name);
        }
    }
}