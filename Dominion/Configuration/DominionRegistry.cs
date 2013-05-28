using StructureMap.Configuration.DSL;

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
        }
    }
}