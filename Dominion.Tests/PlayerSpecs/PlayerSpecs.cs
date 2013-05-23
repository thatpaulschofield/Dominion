using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.AI;
using Dominion.Cards.BasicSet;
using Dominion.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.PlayerSpecs
{
    [TestFixture]
    class PlayerSpecs
    {
        [Test]
        public void Naive_player_should_purchase_first_available_card()
        {
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            player.DrawNewHand();
            var supply = new Supply(new SupplyPile(1, Action.Village));
            var scope = new TurnScope(player, supply, discardPile);
            var turn = new Turn(player, scope);
            turn.Begin();
            player.DiscardPile.ShouldContain(Action.Village);
        }
    }
}
