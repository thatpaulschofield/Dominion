using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Infrastructure;
using StructureMap.Configuration.DSL;
using System;

namespace Dominion.Configuration
{
    public class DominionRegistry : Registry
    {
        public DominionRegistry()
        {
            For<GameBuilder>().Singleton().Use<GameBuilder>();
            For<IEventAggregator>().Singleton().Use<EventAggregator>();
            For<SupplyBuilder>().Singleton().Use<SupplyBuilder>();
            For<DeckBuilder>().Singleton().EnrichAllWith(x => x.WithSets(7.Coppers(), 3.Estates()));
            For<IBus>().Singleton().Use<Bus>();
            For<IStartedBy<MinePlayedMessage>>().Use<MineSaga>();
        }
    }
}