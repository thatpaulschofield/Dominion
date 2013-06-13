using System;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using Action = Dominion.Cards.BasicSet.Action;

namespace Dominion.Tests.PlayerSpecs
{
    [TestFixture]
    public class PlayerSpecs : AbstractPlayerSpec
    {
        private Player _player;
        private MockTurnScope _scope;
        private MockEventAggregator _eventAggregator;
        private Supply _supply;
        private Exception _thrown;

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            base.ConfigureContainer(container);

            new Bootstrapper().BootstrapApplication(container);
                container.Configure(cfg =>
                    {
                        cfg.For<PlayerBuilder>()
                           .EnrichAllWith(x => x.ForSpec(new PlayerSpec().WithController(new NaivePlayerController())));
                        cfg.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();

                        //cfg.For<IPlayerController>().Use<NaivePlayerController>();
                    });

                _eventAggregator = container.GetInstance<IEventAggregator>() as MockEventAggregator;
            container.SetDefaultsToProfile("UnitTests");
        }

        protected override void Given()
        {
            try
            {
                var game = Container.GetInstance<Game>();

                _supply = Container.GetInstance<SupplyBuilder>().BuildSupply();
                _scope = new MockTurnScope
                {
                    TurnNumber = 1,
                    Player = SUT,
                    Supply = _supply,
                    TreasuresInHand = 5.Coppers(),
                    Coins = 5,
                    EventAggregator = _eventAggregator
                };
                _player = game.Players.ToList()[0];

                _player.BeginBuyPhase(new BuyPhase(_scope));
            }
            catch (Exception ex)
            {
                _thrown = ex;
            }
        }

        [Test]
        public void Purchased_card_should_be_in_the_supply()
        {
            var purchasedCards = new CardSet(_scope.PurchasedCards);

            purchasedCards.ForEach(c => (_supply[c] == null).ShouldBeFalse());
        }
    }
}
