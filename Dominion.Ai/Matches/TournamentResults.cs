using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dominion.Ai.Populations;
using System.Linq;

namespace Dominion.Ai.Matches
{
    public class TournamentResults : List<RoundResults>
    {
        public TournamentResults(IEnumerable<RoundResults> roundResults)
        {
            this.AddRange(roundResults);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Tournament Results:");
            int i = 0;
            ForEach(round => sb.AppendLine(String.Format("Round {0}\n{1}", ++i, round)));
            return sb.ToString();
        }

        public Population GetRankedResults()
        {
            var rankedResults = new Population();
            var invertedRounds = new List<RoundResults>(((IEnumerable<RoundResults>) this).Reverse());
            foreach (var round in invertedRounds)
            {
                foreach (var winner in round.Winners)
                {
                    if (!rankedResults.Contains(winner))
                        rankedResults.Add(winner);
                }
            }
            return rankedResults;
        }
    }
}