using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion
{
    public class GameScore : List<PlayerScore>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("  Game scores:");
            ForEach(sc => sb.AppendLine(sc.ToString()));
            return sb.ToString();
        }

        public string Winner
        {
            get { return this.OrderByDescending(x => x.Score).First().PlayerName; }
        }

        public Player WinningPlayer
        {
            get { return this.OrderByDescending(x => x.Score).First().Player; }
        }
    }
}