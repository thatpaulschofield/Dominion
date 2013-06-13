using System.Collections.Generic;
using System.Text;

namespace Dominion.Ai.Matches
{
    public class SeriesScores : List<GameScore>
    {
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine("Series scores:");
            ForEach(sc => s.AppendLine(sc.ToString()));
            return s.ToString();
        }
    }
}