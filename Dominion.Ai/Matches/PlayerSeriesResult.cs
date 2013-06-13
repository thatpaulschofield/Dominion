using System;

namespace Dominion.Ai.Matches
{
    public class PlayerSeriesResult
    {
        public string Player { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public decimal WinRate { get; set; }

        public override string ToString()
        {
            return String.Format("Player: {0} Wins: {1} Losses: {2} Win rate: {3:P2}", Player, Wins, Losses,
                                 WinRate);
        }
    }
}