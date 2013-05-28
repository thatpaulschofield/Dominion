using System;

namespace Dominion.GameEvents
{
    public class DeckDepletedEvent : GameCommand
    {
        public DeckDepletedEvent(ITurnScope turnScope) : base(turnScope)
        {
            _availableResponses.Add(new ShuffleDeckResponse(turnScope));
        }

        public override string ToString()
        {
            return String.Format("Deck was depleted for player {0}", TurnScope.Player.Name);
        }
    }
}