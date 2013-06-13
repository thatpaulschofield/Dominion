using System;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using SpecsFor;
using StructureMap;

namespace Dominion.Tests.PlayerSpecs
{
    public class When_another_players_deck_is_depleted : SpecsFor<Player>
    {
        private MockEventAggregator _eventAggregator;
        private IContainer _container;
        private Exception _thrown;

        protected override void ConfigureContainer(IContainer container)
        {
            try
            {
                _container = container;
                container.Configure(cfg =>
                {
                    cfg.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
                    cfg.For<Deck>().Singleton().Use(new Deck(7.Coppers(), 3.Estates()));
                    cfg.For<DiscardPile>().Use<DiscardPile>();
                    cfg.For<Player>().Use<Player>().Ctor<string>().Is("Test player");
                    cfg.For<Guid>().Use(Guid.NewGuid);
                });
                _eventAggregator = container.GetInstance<IEventAggregator>() as MockEventAggregator;
            }
            catch (Exception ex)
            {
                _thrown = ex;
            }
        }

        protected override void When()
        {
            try
            {
                SUT.Handle(new DeckDepletedEvent(new MockTurnScope { Player = _container.GetInstance<Player>(), EventAggregator = _eventAggregator }));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [Test]
        public void Test_setup_should_succeed()
        {
            if (_thrown != null)
            {
                Console.WriteLine(_thrown);
                _thrown.ShouldBeNull();
            }
        }

        [Test]
        public void player_should_not_replenish_deck()
        {
            _eventAggregator.AssertMessageWasNotSent<DeckReplenishedEvent>();
        }

    }
}