using Dominion.AI;
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
            var turnScope = new TurnScope(new Supply(), new DiscardPile());
            var actionPhase = new ActionPhase(turnScope,
                new CardSet(Action.Village, Action.Village));
            var response = actionPhase.PlayAction(Treasure.Copper);
            response.ShouldBeType<SkipActionPhaseResponse>();
        }

        [Test]
        public void When_an_valid_action_card_is_selected_action_the_action_will_be_played()
        {
            var turnScope = new TurnScope(new Supply(), new DiscardPile());
            var actionPhase = new ActionPhase(turnScope,
                new CardSet(Action.Village, Action.Village));
            var response = actionPhase.PlayAction(Action.Village);
            response.ShouldBeType<PlayActionResponse>();
            ((PlayActionResponse)response).Card.ShouldEqual(Action.Village);
        }
    }
}
