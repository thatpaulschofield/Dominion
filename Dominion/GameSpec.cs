using System;
using System.Collections.Generic;

namespace Dominion
{
    public class GameSpec
    {
        public GameSpec()
        {
            Players = new List<PlayerSpec>();
        }

        public List<PlayerSpec> Players { get; private set; }

        public GameSpec WithConsolePlayer(string playerName)
        {
            Players.Add(new PlayerSpec().WithPlayerType(PlayerType.Console).WithPlayerName(playerName));
            return this;
        }
    }
}