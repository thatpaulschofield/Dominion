using System;
using Dominion.AI;
using Dominion.Ai.ConstantValueProviders;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions.Numeric;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.BasicSet;
using Dominion.GameEvents;
using Dominion.Tests;
using Dominion.Tests.GameEvents;
using StructureMap.Configuration.DSL;

namespace Dominion.Ai
{
    public class AiRegistry : Registry
    {
        public AiRegistry()
        {
            For<FullTreeStrategy>().Use<FullTreeStrategy>();
            For<NodeRegistry>().Use<NodeRegistry>();
            
            For<IInitialValueProvider>().AlwaysUnique().Use<IntValueProvider>();
            For<IInitialValueProvider>().AlwaysUnique().Use<CardTypeValueProvider>();
            For<IInitialValueProvider>().AlwaysUnique().Use<CardValueProvider>();
            For<IInitialValueProvider>().Use<EventResponseCriteriaProvider>();
            For<IInitialValueProvider>().Use<ResponseVotesValueProvider>();
            For<IInitialValueProvider>()
                .Use(new InitialValueProvider<bool>
                    { ProvideValueInitializer = () => new Random((int) DateTime.Now.Ticks).Next(0, 1) == 0 });
            For<IValueProviderRegistry>().Use<ValueProviderRegistry>();
            
            For<ITurnScope>().Use<TurnScope>();
            For<IEventAggregator>().Use<EventAggregator>();
            For<Random>().Singleton().Use(ctx => new Random((int) DateTime.Now.Ticks));
            Profile("UnitTests", p =>
                {
                    var unitTestRandom = new Random();
                    p.For<Random>().Use(ctx => unitTestRandom);
                    p.For<ITurnScope>().Use<MockTurnScope>();
                    p.For<IEventAggregator>().Use<MockEventAggregator>();
                });
            Profile("FunctionalTests", p =>
                {
                    var unitTestRandom = new Random((int) DateTime.Now.Ticks);
                    p.For<Random>().Use(ctx => unitTestRandom);
                });
            For<CardTypeValueProvider>().AlwaysUnique().Use<CardTypeValueProvider>();
            For<Card>().Use(ctx => 
                ctx.GetInstance<CardTypeValueProvider>().ProduceValue() as CardType
                );
            For<int>().Use(ctx => (int) ctx.GetInstance<IntValueProvider>().ProduceValue());
            For<ResponseVotes>().Use<ResponseVotes>();

        }
    }
}
