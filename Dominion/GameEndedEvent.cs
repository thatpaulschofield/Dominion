using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion
{
    internal class GameEndedEvent : GameMessage
    {
        private readonly IEnumerable<PlayerScore> _scores;

        public GameEndedEvent(IEnumerable<PlayerScore> scores, ITurnScope turnScope) : base(turnScope)
        {
            _scores = scores;
        }
    }
}