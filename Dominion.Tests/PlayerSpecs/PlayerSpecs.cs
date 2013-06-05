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

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            base.ConfigureContainer(container);
            container.Configure(cfg =>
                {
                    cfg.For<Player>().Use<Player>().Ctor<string>().Is("Test player");
                    cfg.For<Deck>().Singleton().Use(new Deck(7.Coppers(), 3.Estates()));
                    cfg.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
                    cfg.For<Supply>().Singleton()
                       .Use(c => new Supply(new SupplyPile(1, Action.Village, c.GetInstance<IEventAggregator>())));
                    cfg.For<DiscardPile>().Singleton().Use<DiscardPile>();
                    cfg.For<IPlayerController>().Use<NaivePlayerController>();
                });
            _eventAggregator = container.GetInstance<IEventAggregator>() as MockEventAggregator;
        }

        protected override void Given()
        {
            var game = Container.GetInstance<Game>();

            _supply = Container.GetInstance<SupplyBuilder>().BasicGame().WithSet<FirstGame>().WithPlayers(2).BuildSupply();
            _scope = new MockTurnScope
                {
                    TurnNumber = 1,
                    ActingPlayer = SUT,
                    Supply = _supply,
                    TreasuresInHand = 5.Coppers(),
                    Coins = 5,
                    EventAggregator = _eventAggregator
                };
            _player = game.Players.ToList()[0];

            _player.BeginBuyPhase(new BuyPhase(_scope));
        }

        [Test]
        public void Container_should_be_configured_correctly()
        {
            Container.AssertConfigurationIsValid();
        }

        [Test]
        public void Purchased_card_should_be_in_the_supply()
        {
            var purchasedCards = new CardSet(_scope.PurchasedCards);

            purchasedCards.ForEach(c => (_supply[c] == null).ShouldBeFalse());
        }
    }
}
