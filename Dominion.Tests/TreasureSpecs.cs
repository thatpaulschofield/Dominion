using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    public class TreasureSpecs
    {
        [Test]
        public void Copper_should_cost_0()
        {
            Treasure.Copper.Create().Cost.ShouldEqual(0.Coins());
        }

        [Test]
        public void Copper_should_be_worth_1_coin()
        {
            Treasure.Copper.Create().Coins.ShouldEqual(1.Coins());
        }

        [Test]
        public void Silver_should_cost_3_coins()
        {
            Treasure.Silver.Create().Cost.ShouldEqual(3.Coins());
        }

        [Test]
        public void Silver_should_be_worth_2_coins()
        {
            Treasure.Silver.Create().Coins.ShouldEqual(2.Coins());
        }

        [Test]
        public void Gold_should_cost_6_coins()
        {
            Treasure.Gold.Create().Cost.ShouldEqual(6.Coins());
        }

        [Test]
        public void Gold_should_be_worth_3_coins()
        {
            Treasure.Gold.Create().Coins.ShouldEqual(3.Coins());
        }

        [Test]
        public void Copper_should_equal_a_copper()
        {
            Treasure.Copper.Create().ShouldEqual(Treasure.Copper.Create());
        }

        [Test]
        public void Silver_should_equal_a_silver()
        {
            Treasure.Silver.Create().ShouldEqual(Treasure.Silver.Create());
        }

        [Test]
        public void Gold_should_equal_a_gold()
        {
            Treasure.Gold.Create().ShouldEqual(Treasure.Gold.Create());
        }
    }
}