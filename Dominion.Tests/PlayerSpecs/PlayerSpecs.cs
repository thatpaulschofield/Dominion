using System;
using System.Linq;
using Dominion.Cards;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using Action = Dominion.Cards.BasicSet.Action;

namespace Dominion.Tests.PlayerSpecs
{
    [TestFixture]
    class PlayerSpecs : AbstractPlayerSpec
    {
        private Player _player;
        private MockTurnScope _scope;

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            try
            {
                base.ConfigureContainer(container);
                container.Configure(c =>
                {
                    c.For<Game>().Singleton().Use(ctx => ctx.GetInstance<GameBuilder>().Initialize(1));
                    c.For<Deck>().Singleton().Use(new Deck(7.Coppers(), 3.Estates()));
                    c.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
                    c.For<Supply>().Singleton().Use(cfg => new Supply(new SupplyPile(1, Action.Village, cfg.GetInstance<IEventAggregator>())));
                    c.For<DiscardPile>().Singleton().Use<DiscardPile>();
                    c.For<SupplyBuilder>().Singleton().Use<SupplyBuilder>().EnrichWith(x =>
                        x.BasicGame().With(1.Of(Action.Village)));
                });
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected override void Given()
        {
            var game = Container.GetInstance<Game>();

            var supply = Container.GetInstance<Supply>();
            _scope = new MockTurnScope
                {
                    TurnNumber = 1,
                    Supply = supply,
                    PotentialCoins = 5,
                    TreasuresInHand = 5.Coppers()
                };
            _player = game.Players.ToList()[0];

            _player.BeginTurn(_scope);
        }

        [Test]
        public void Container_should_be_configured_correctly()
        {
            Container.AssertConfigurationIsValid();
        }

        [Test]
        public void Naive_player_should_purchase_a_card()
        {
            _scope.PurchasedCards.ShouldContain<Card>(Action.Village);
        }
    }
}
