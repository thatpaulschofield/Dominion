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
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            var turnScope = new TurnScope(player, new Supply(new SupplyPile(1, Action.Village), new SupplyPile(1, Treasure.Copper)), new DiscardPile());
            var buyPhase = new BuyPhase(turnScope);
            var hand = new Hand();
            var response = buyPhase.BuyCard(Victory.Province, player.Hand.Treasures());
            response.ShouldBeType<SkipBuyPhaseResponse>();
        }

        [Test]
        public void When_a_valid_card_is_selected_the_card_will_be_purchased()
        {
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            player.DrawNewHand();
            var turnScope = new TurnScope(player, new Supply(new SupplyPile(1, Action.Village), new SupplyPile(1, Treasure.Copper)), new DiscardPile());
            var buyPhase = new BuyPhase(turnScope);
            var response = buyPhase.BuyCard(Action.Village, player.Hand.Treasures());
            response.ShouldBeType<BuyCardResponse>();
            ((BuyCardResponse)response).CardToPurchase.ShouldEqual(Action.Village);
        }
    }
}