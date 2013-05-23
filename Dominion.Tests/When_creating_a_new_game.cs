using System.Linq;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.VictoryCards;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    [TestFixture]
    public class When_creating_a_new_game
    {
        [TestCase(2, 12, 10)]
        [TestCase(3, 8, 20)]
        [TestCase(4, 8, 30)]
        public void NewGame(int players, int victoryCards, int curseCards)
        {
            var game = Game.Initialize(players);
            game.Supply[Victory.Estate].Count.ShouldEqual(victoryCards, "Wrong number of estates");
            game.Supply[Victory.Duchy].Count.ShouldEqual(victoryCards, "Wrong number of duchies");
            game.Supply[Victory.Province].Count.ShouldEqual(victoryCards, "Wrong number of provinces");
            game.Supply[BasicCards.Curse].Count.ShouldEqual(curseCards, "Wrong number of curse cards");
            game.Players.Count.ShouldEqual(players, "Wrong number of players");
            game.Players.ForEach(p =>
                {
                    p.Hand.Count().ShouldEqual(5, "Wrong number of cards in hand");
                    p.Deck.Count().ShouldEqual(5, "Wrong number of cards in deck");
                });

        }
    }
}