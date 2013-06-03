using System;
using Dominion.GameEvents;

namespace Dominion
{
    public class DeckReplenishedEvent : GameMessage
    {
        public DeckReplenishedEvent(IActionScope turnScope):base(turnScope)
        {
            
        }
        public override string ToString()
        {
            return String.Format("{0} shuffled deck.", ActionScope.ActingPlayer.Name);
        }
    }
}