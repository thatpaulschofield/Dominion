using System;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.Populations;
using Dominion.Ai.TreeBuilding;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    public class PopulationBuilderSpecs
    {
        private Population _population;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            var container = new Bootstrapper().BootstrapApplication();
            var builder = container.GetInstance<PopulationBuilder<CombineVotes,FullTreeStrategy>>();
            _population = builder.BuildPopulation(5, new TreeSpec{MaxDepth = 25});
        }

        [Test]
        public void The_population_should_be_the_correct_size()
        {
            _population.Count.ShouldEqual(5);
        }

        [Test]
        public void The_population_should_be_ready()
        {
            var scope = new MockTurnScope(new MockEventAggregator());
            var message = new NullResponse(scope);
            _population.ForEach(s => Console.WriteLine(s.HandleGameEvent(message, scope)));
        }
    }
}