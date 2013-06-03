using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayerGainedCardEvent : GameMessage
    {
        private readonly CardType _card;

        public PlayerGainedCardEvent(CardType card, ITurnScope turnScope) : base(turnScope)
        {
            _card = card;
        }

        public Card Card { get { return _card; } }
    }
}