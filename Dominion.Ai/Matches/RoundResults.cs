using System.Collections.Generic;
using System.Text;
using Dominion.Ai.Populations;

namespace Dominion.Ai.Matches
{
    public class RoundResults
    {
        public RoundResults()
        {
            Winners = new Population();
            Scores = new SeriesResults();
        }
        public Population Winners { get; set; }

        public SeriesResults Scores { get; set; }

        public override string ToString()
        {
            int i = 0;
            var sb = new StringBuilder();
            sb.AppendLine("Round Results:");
            Winners.ForEach(winner =>
                {
                    i++;
                    sb.AppendLine("Game " + i + " - winner: " + winner.ToString());
                });
            return sb.ToString();
        }
    }
}
