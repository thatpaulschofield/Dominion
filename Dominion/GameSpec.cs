using System;
using System.Collections.Generic;
using Dominion.Cards;

namespace Dominion
{
    public class GameSpec
    {
        public GameSpec()
        {
            Players = new List<PlayerSpec>();
        }

        public List<PlayerSpec> Players { get; private set; }


        public GameSpec WithPlayer(params PlayerSpec[] playerSpecs)
        {
            playerSpecs.ForEach(spec => Players.Add(spec));
            return this;
        }

        public GameSpec BasicGame()
        {
            IsBasicGame = true;
            return this;
        }

        public bool IsBasicGame { get; set; }

        public DeckSet DeckSet { get; private set; }

        public GameSpec WithSet<T>() where T : DeckSet, new()
        {
            DeckSet = new T();
            return this;
        }
    }

    public class Competitor
    {

    }
}