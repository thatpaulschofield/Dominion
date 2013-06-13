using System;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayerGainedCardEvent : GameMessage
    {
        private readonly CardType _card;

        public PlayerGainedCardEvent(CardType card, IActionScope turnScope) : base(turnScope)
        {
            _card = card;
            Description = String.Format("{0}: gained {1}",
                                        (turnScope == null || turnScope.Player == null)
                                        ? "none" : turnScope.Player.Name, 
                                        card.Create().Name);
        }

        public Card Card { get { return _card; } }
    }
}