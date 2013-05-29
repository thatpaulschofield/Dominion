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
        public void When_no_card_is_available_to_purchase_phase_ends()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController(), eventAggregator);
            var supply = new Supply(

                new SupplyPile(1, Action.Village, eventAggregator));

            var turnScope = new MockTurnScope {TreasuresInHand = player.Hand, Coins = 0, Player = player, Supply = supply, EventAggregator = eventAggregator};
            var buyPhase = new BuyPhase(turnScope);
            player.BeginBuyPhase(buyPhase);
            eventAggregator.AssertMessageWasSent<SkipBuyPhaseResponse>();
        }

        [Test]
        public void When_a_valid_card_is_selected_the_card_will_be_purchased()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController(), eventAggregator);
            player.DrawNewHand(new TurnScope(player, new Supply(new SupplyPile(1, Action.Village, eventAggregator), new SupplyPile(1, Treasure.Copper, eventAggregator)), eventAggregator));
            var turnScope = new TurnScope(player, new Supply(new SupplyPile(1, Action.Village, eventAggregator), new SupplyPile(1, Treasure.Copper, eventAggregator)), eventAggregator);
            var buyPhase = new BuyPhase(turnScope);
            player.BeginBuyPhase(buyPhase);
            eventAggregator.AssertMessageWasSent<BuyCardResponse>();
        }
    }
}