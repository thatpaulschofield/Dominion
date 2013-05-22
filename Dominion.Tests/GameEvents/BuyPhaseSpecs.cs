using Dominion.AI;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using Dominion.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.GameEvents
{
    public class BuyPhaseSpecs
    {
        [Test]
        public void When_an_illegal_card_is_selected_buy_phase_ends()
        {
            var turnScope = new TurnScope(new Supply(), new DiscardPile());
            var buyPhase = new BuyPhase(turnScope,
                                        new CardSet(Action.Village, Treasure.Copper));
            var response = buyPhase.BuyCard(Victory.Province);
            response.ShouldBeType<SkipBuyPhaseResponse>();
        }

        [Test]
        public void When_a_valid_card_is_selected_the_card_will_be_purchased()
        {
            var turnScope = new TurnScope(new Supply(), new DiscardPile());
            var buyPhase = new BuyPhase(turnScope,
                                        new CardSet(Action.Village, Treasure.Copper));
            var response = buyPhase.BuyCard(Action.Village);
            response.ShouldBeType<BuyCardResponse>();
            ((BuyCardResponse)response).CardToPurchase.ShouldEqual(Action.Village);
        }
    }
}