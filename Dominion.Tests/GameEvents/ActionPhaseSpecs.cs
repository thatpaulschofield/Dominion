using Dominion.AI;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.GameEvents
{
    [TestFixture]
    class ActionPhaseSpecs
    {
        [Test]
        public void When_an_illegal_card_is_selected_action_phase_ends()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController(), eventAggregator);
            var turnScope = new TurnScope(player, new Supply(), eventAggregator);
            var actionPhase = new ActionPhase(turnScope,
                new CardSet(Action.Village, Action.Village));
            var response = actionPhase.PlayAction(Treasure.Copper);
            response.ShouldBeType<SkipActionPhaseResponse>();
        }

        [Test]
        public void When_an_valid_action_card_is_selected_action_the_action_will_be_played()
        {
            var eventAggregator = new MockEventAggregator();
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController(), eventAggregator);
            var turnScope = new TurnScope(player, new Supply(), eventAggregator);
            var actionPhase = new ActionPhase(turnScope,
                new CardSet(Action.Village, Action.Village));
            var response = actionPhase.PlayAction(Action.Village);
            response.ShouldBeType<PlayActionResponse>();
            ((PlayActionResponse)response).Card.ShouldEqual<Card>(Action.Village);
        }
    }
}
