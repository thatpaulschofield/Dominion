using System;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class SupplyPileDepletedEvent : GameMessage
    {
        public readonly CardType Type;

        public SupplyPileDepletedEvent(CardType type, IActionScope turnScope) : base(turnScope)
        {
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("Supply of {0} cards is empty", Type.Create());
        }
    }
}