using Dominion.Cards;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace Dominion.Tests
{
    public class When_querying_supply_for_cards_eligible_for_purchase : SpecsFor<Supply>
    {
        private CardSet _eligibleCards;

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            var builder = new SupplyPileBuilder(new MockEventAggregator());
            container.Configure(cfg => cfg.For<Supply>().Use(
                new Supply(
                    builder.OfSize(1).OfType(Treasure.Copper),
                    builder.OfSize(0).OfType(Victory.Estate),
                    builder.OfSize(1).OfType(Victory.Province)
                    )));
        }

        protected override void When()
        {
            var turnScope = new MockTurnScope {Coins = 2};
            _eligibleCards = SUT.FindCardsEligibleForPurchase(turnScope);
        }

        [Test]
        public void Should_not_return_cards_that_cant_be_bought()
        {
            _eligibleCards.ShouldNotContain<Card>(Victory.Province);
        }

        [Test]
        public void Should_return_non_empty_stacks()
        {
            _eligibleCards.ShouldContain<Card>(Treasure.Copper);
        }

        [Test]
        public void Should_not_return_empty_stacks()
        {
            _eligibleCards.ShouldNotContain<Card>(Victory.Estate);
        }
    }
}