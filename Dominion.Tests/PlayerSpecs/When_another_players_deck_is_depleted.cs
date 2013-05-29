using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using SpecsFor;
using StructureMap;

namespace Dominion.Tests.PlayerSpecs
{
    public class When_another_players_deck_is_depleted : SpecsFor<Player>
    {
        private MockEventAggregator _eventAggregator;
        private IContainer _container;

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            container.Configure(cfg =>
                {
                    cfg.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
                    cfg.For<Deck>().Singleton().Use(new Deck(7.Coppers(), 3.Estates()));
                    cfg.For<DiscardPile>().Use<DiscardPile>();
                    cfg.For<Player>().Use<Player>().Ctor<string>().Is("Test player");
                });
            _container = container;
            _eventAggregator = container.GetInstance<IEventAggregator>() as MockEventAggregator;
        }

        protected override void When()
        {
            SUT.Handle(new DeckDepletedEvent(new MockTurnScope {Player = _container.GetInstance<Player>()}));
        }

        [Test]
        public void player_should_not_replenish_deck()
        {
            _eventAggregator.AssertMessageWasNotSent<DeckReplenishedEvent>();
        }

    }
}