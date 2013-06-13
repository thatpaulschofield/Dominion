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

        public override string ToString()
        {
            string s = "Game ended.  Scores:\n";
            _scores.ForEach(sc => s = s + sc + "\n");
            return s;
        }
    }
}