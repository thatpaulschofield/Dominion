using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.VictoryCards;
using Dominion.Infrastructure;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using StructureMap;

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
            IEventAggregator eventAggregator = new MockEventAggregator();
            var deckBuilder = new DeckBuilder();
            var supplyBuilder = new SupplyBuilder(eventAggregator);
            var playerBuilder = new PlayerBuilder(eventAggregator, deckBuilder);
            var scope = new GameScope(eventAggregator, supplyBuilder, playerBuilder, new List<EndGameCondition>(), new MockBus(), new Container());
            GameSpec gameSpec = new GameSpec()
                .BasicGame()
                .WithSet<FirstGame>()
                .WithPlayer(players.Times(()=>new PlayerSpec()));

            Game game = new GameBuilder().Initialize(gameSpec, scope);
            game.Supply[Victory.Estate].Count.ShouldEqual(victoryCards, "Wrong number of estates");
            game.Supply[Victory.Duchy].Count.ShouldEqual(victoryCards, "Wrong number of duchies");
            game.Supply[Victory.Province].Count.ShouldEqual(victoryCards, "Wrong number of provinces");
            game.Supply[BasicCards.Curse].Count.ShouldEqual(curseCards, "Wrong number of curse cards");
            game.Players.Count().ShouldEqual(players, "Wrong number of players");
        }
    }

    public class MockBus : IBus
    {
    }
}