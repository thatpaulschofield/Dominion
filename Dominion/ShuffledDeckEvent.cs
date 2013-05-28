using System;
using Dominion.GameEvents;

namespace Dominion
{
    public class ShuffledDeckEvent : GameMessage
    {
        public ShuffledDeckEvent(ITurnScope turnScope):base(turnScope)
        {
            
        }
        public override string ToString()
        {
            return String.Format("{0} shuffled deck.", TurnScope.Player.Name);
        }
    }
}