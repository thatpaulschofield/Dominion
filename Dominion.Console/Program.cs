using System;
using Dominion.AI;
using Dominion.Ai;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using Dominion.Cards.Intrigue.DeckSets;
using Dominion.PlayerControllers.Console;
using StructureMap;

namespace Dominion.Console
{
    class Program
    {
        private static IContainer _container;

        private static void Main(string[] args)
        {
            try
            {
                int aiPlayerCount = AskForAiPlayerCount();
                _container = new Bootstrapper().BootstrapApplication();

                var aiPlayerBuilder = _container.GetInstance<AiPlayerBuilder>();

                var gameSpec = new GameSpec()
                    .BasicGame()
                    //.WithSet<FirstGame>()
                    //.WithSet<BigMoney>()
                    //.WithSet<Interaction>()
                    //.WithSet<SizeDistortion>()
                    .WithSet<Deconstruction>()
                    .WithPlayer(_container.GetInstance<PlayerSpec<ConsolePlayerController>>().WithPlayerName("You"))
                    ;
                var i = 0;
                aiPlayerCount.Times(() =>
                    {
                        gameSpec.WithPlayer(aiPlayerBuilder.WithTreeSpec(new TreeSpec{MaxDepth = 100}).BuildAiPlayer("AI - " + ++i));
                    });
                var gameBuilder = new GameBuilder();
                var scope = _container.GetInstance<GameScope>();
                new ConsoleEventLogger(scope.EventAggregator);
                Game game = gameBuilder.Initialize(gameSpec, scope);

                game.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static int AskForAiPlayerCount()
        {
            System.Console.WriteLine("How many AI players would you like to play against?");
            int players = 0;
            do
            {
                Int32.TryParse(System.Console.ReadLine(), out players);
            } while (players < 1 && players > 4 );
            return players;
        }
    }
}
