using System.Linq;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    [TestFixture]
    public class GameSpecs
    {
        private Deck _deck;

        [SetUp]
        public void SetUp()
        {
            _deck = Game.DealStartupDeck();
        }

        [Test]
        public void Startup_deck_should_contain_10_cards()
        {
            _deck.Count().ShouldEqual(10);
        }

        [Test]
        public void Startup_deck_should_contain_7_coppers()
        {
            _deck[Treasure.Copper].Count().ShouldEqual(7);
        }

        [Test]
        public void Startup_deck_should_contain_3_estates()
        {
            _deck[Victory.Estate].Count().ShouldEqual(3);
        }
    }
}
