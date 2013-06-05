using System.Collections.Generic;
using Dominion.Cards.BasicSet.Actions.Saga;
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
            For<IBus>().Singleton().Use<Bus>();

            For<EndGameCondition>().Use<ThreeSupplyPilesDepletedEndGameCondition>();
            For<IEnumerable<EndGameCondition>>().Use(cfg => 
                cfg.GetAllInstances<EndGameCondition>()
                );
        }
    }
}