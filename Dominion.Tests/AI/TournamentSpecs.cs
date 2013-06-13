using System;
using Dominion.Ai.Matches;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.Populations;
using Dominion.Ai.TreeBuilding;
using NUnit.Framework;
using Should;
using StructureMap;

namespace Dominion.Tests.AI
{
    public class TournamentSpecs
    {
        private PopulationBuilder<CombineVotes, FullTreeStrategy> _builder;
        private IContainer _container;
        private ITournamentBuilder _tournamentBuilder;


        [SetUp]
        public void SetUp()
        {
            _container = new Bootstrapper().BootstrapApplication();
            _builder = _container.GetInstance<PopulationBuilder<CombineVotes, FullTreeStrategy>>();
            _tournamentBuilder = _container.GetInstance<ITournamentBuilder>();
        }


        [Test]
        public void Tournament_with_8_competitors_and_2_player_games_should_have_3_rounds()
        {
            Tournament_should_have_correct_number_of_rounds_for_population_and_game_size(8, 2).ShouldEqual(3);
        }

        [Explicit]
        [TestCase(2, 2, Result = 1)]
        [TestCase(4, 2, Result = 2)]
        [TestCase(8, 2, Result = 3)]
        [TestCase(16, 2, Result = 4)]
        [TestCase(32, 2, Result = 5)]
        [TestCase(64, 2, Result = 6)]
        [TestCase(4, 4, Result = 1)]
        [TestCase(16, 4, Result = 2)]
        [TestCase(32, 4, Result = 3)]
        [TestCase(256, 4, Result = 4)]
        public int Tournament_should_have_correct_number_of_rounds_for_population_and_game_size(int popSize, int gameSize)
        {
            var tournament = CreateTournament(popSize, gameSize, 1);
            tournament.Begin();
            var results = tournament.GetResults();
            Console.WriteLine(results);
            return results.Count;
        }

        private Tournament CreateTournament(int populationSize, int gameSize, int seriesSize)
        {
            var population = _builder.BuildPopulation(populationSize, new TreeSpec {MaxDepth = 2});
            var tournament = _tournamentBuilder.Build(seriesSize, gameSize, population);
            return tournament;
        }
    }


}