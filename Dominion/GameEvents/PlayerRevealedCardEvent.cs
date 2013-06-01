using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayerRevealedCardEvent : GameMessage
    {
        private readonly Card _card;

        public PlayerRevealedCardEvent(IActionScope actionScope, Card card) : base(actionScope)
        {
            _card = card;
        }

        public override string ToString()
        {
            return ActionScope.Player.Name + " revealed " + _card.Name;
        }
    }
}