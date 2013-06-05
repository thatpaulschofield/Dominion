using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.GameEvents;
using NUnit.Framework;

namespace Dominion.Tests.GameEvents
{
    public class BuyPhaseSpecs
    {
        [Test]
        public void When_no_card_is_available_to_purchase_phase_ends()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            var supply = new Supply(

                new SupplyPile(1, Action.Village, eventAggregator));

            var turnScope = new MockTurnScope {TreasuresInHand = player.Hand, Coins = 0, ActingPlayer = player, Supply = supply, EventAggregator = eventAggregator};
            var buyPhase = new BuyPhase(turnScope);
            player.BeginBuyPhase(buyPhase);
            eventAggregator.AssertMessageWasSent<DeclineToPurchaseResponse>();
        }

        [Test]
        public void When_a_valid_card_is_selected_the_card_will_be_purchased()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            var supply = new Supply(new SupplyPile(1, Treasure.Silver, eventAggregator), new SupplyPile(1, Treasure.Copper, eventAggregator));

            var scope = new TurnScope(player, supply, eventAggregator);
            player.DrawNewHand(scope);
            player.PlayTreasures(player.Hand.Treasures(), scope);
            var buyPhase = new BuyPhase(scope);
            player.BeginBuyPhase(buyPhase);
            eventAggregator.AssertMessageWasSent<PlayerGainedCardEvent>();
        }
    }
}