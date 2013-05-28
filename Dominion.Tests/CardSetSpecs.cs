using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    [TestFixture]
    public class CardSetSpecs
    {
        [Test]
        public void When_discarded_card_set_should_be_empty()
        {
            var player = new MockPlayer();
            var cards = new CardSet(Treasure.Copper, Treasure.Gold, Treasure.Silver, Victory.Estate);
            var discard = new DiscardPile();
            cards.DiscardInto(discard, new MockTurnScope());
            cards.ShouldBeEmpty();
        }
    }
}