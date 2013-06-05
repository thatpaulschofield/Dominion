using System;
using Dominion.AI;
using Dominion.Ai.ConstantValueProviders;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions.Numeric;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
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
            base.IncludeRegistry<BasicSetRegistry>();
            For<FullTreeStrategy>().Use<FullTreeStrategy>();
            For<NodeRegistry>().Singleton().Use<NodeRegistry>();
            
            For<IInitialValueProvider>().AlwaysUnique().Use<IntValueProvider>();
            For<IInitialValueProvider>().Singleton().Use<CardTypeValueProvider>();
            For<IInitialValueProvider>().Use<AllResponsesProvider>();
            For<IInitialValueProvider>().Singleton()
                .Use(new InitialValueProvider<bool>
                    { ProvideValueInitializer = () => new Random((int) DateTime.Now.Ticks).Next(0, 1) == 0 });
            For<IValueProviderRegistry>().Singleton().Use<ValueProviderRegistry>();
            For<ITurnScope>().Singleton().Use<MockTurnScope>();
            For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
            For<CardTypeValueProvider>().AlwaysUnique().Use<CardTypeValueProvider>();
            For<Card>().Use(ctx => 
                ctx.GetInstance<CardTypeValueProvider>().ProduceValue() as CardType
                );
            For<int>().Use(ctx => (int) ctx.GetInstance<IntValueProvider>().ProduceValue());
            For<ResponseVotes>().Use<ResponseVotes>();
        }
    }
}
