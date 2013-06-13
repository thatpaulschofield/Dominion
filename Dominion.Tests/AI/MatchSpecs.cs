using System;
using System.Collections.Generic;
using Dominion.AI;
using Dominion.Ai;
using Dominion.Ai.Matches;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using Dominion.PlayerControllers.Console;
using NUnit.Framework;
using StructureMap;

namespace Dominion.Tests.AI
{
    public class MatchSpecs
    {
        private readonly object locker = new object();
        private GameSpec _gameSpec;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {

        }

        [Test]
        public void Can_start_a_series()
        {
            var series = new Series(CreateGame).WithLength(1);
            var scores = series.Start();
        }

        [Test, Explicit]
        public void Can_start_a_larger_series()
        {
            var series = new Series(CreateGames(10)).WithLength(10);
            var scores = series.Start();
            Console.WriteLine(scores);
        }


        [Test, Explicit]
        public void Can_start_a_huge_series()
        {
            var series = new Series(CreateGames(100)).WithLength(100);
            var scores = series.Start();
            Console.WriteLine(scores);
        }

        public IEnumerable<Game> CreateGames(int count)
        {
            var games = new List<Game>();
            count.Times(() => games.Add(CreateGame()));
            return games;
        }

        public Game CreateGame()
        {
            lock (locker)
            {
                var container = new Bootstrapper().BootstrapApplication();
                container.SetDefaultsToProfile("FunctionalTests");
                container.GetInstance<ConsoleEventLogger>();
                var aiPlayerBuilder = container.GetInstance<AiPlayerBuilder>();
                _gameSpec = new GameSpec()
                    .WithPlayer(aiPlayerBuilder.BuildAiPlayer("AI - 1", new Guid()))
                    .WithPlayer(aiPlayerBuilder.BuildAiPlayer("AI - 2", new Guid()))
                    .BasicGame()
                    .WithSet<FirstGame>(); 
                var gameBuilder = new GameBuilder();
                var game = gameBuilder.Initialize(_gameSpec, container.GetInstance<GameScope>());
                return game;
            }
        }
    }
}
