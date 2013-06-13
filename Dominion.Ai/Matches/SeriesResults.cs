using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Ai.Matches
{
    public class SeriesResults : List<GameScore>
    {

        public IEnumerable<PlayerSeriesResult> GetResults()
        {
            var totalGames = this.Count;

            return from foo in this
                   group foo by foo.Winner
                   into playerGroup
                   let wins = playerGroup.Count()
                   let losses = totalGames - wins
                   let winRate = (decimal)wins/(decimal)totalGames
                   select new PlayerSeriesResult
                       {
                           Player = playerGroup.Key,
                           Wins = wins,
                           Losses = losses,
                           WinRate = winRate
                       };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Series Results:");
            sb.AppendLine("===============");
            GetResults().ForEach(r => sb.AppendLine(r.ToString()));
            return sb.ToString();
        }
    }
}