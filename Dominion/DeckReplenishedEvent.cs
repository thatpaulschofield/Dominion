using System;
using Dominion.GameEvents;

namespace Dominion
{
    public class DeckReplenishedEvent : GameMessage
    {
        public DeckReplenishedEvent(ITurnScope turnScope):base(turnScope)
        {
            
        }
        public override string ToString()
        {
            return String.Format("{0} shuffled deck.", TurnScope.Player.Name);
        }
    }
}