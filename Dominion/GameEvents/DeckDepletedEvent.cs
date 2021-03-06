﻿using System;

namespace Dominion.GameEvents
{
    public class DeckDepletedEvent : GameCommand
    {
        public DeckDepletedEvent(IActionScope turnScope)
            : base(turnScope)
        {
            _availableResponses.Add(new ShuffleDeckResponse(turnScope));
        }

        public override string ToString()
        {
            return String.Format("{0}'s deck was depleted", TurnScope.Player.Name);
        }
    }
}