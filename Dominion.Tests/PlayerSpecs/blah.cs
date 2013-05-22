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
    class blah
    {
        [Test]
        public void foo()
        {
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            var player = new Player(deck, discardPile, new NaivePlayerController());
            var supply = new Supply(new SupplyPile(1, Action.Village));
            var scope = new TurnScope(supply, discardPile);
            var turn = new Turn(player, scope);
            turn.Begin();
            player.DiscardPile.ShouldContain(Action.Village);
        }
    }
}
