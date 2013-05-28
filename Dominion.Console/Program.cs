using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards;
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
                _container = new Bootstrapper().BootstrapApplication();
                _container.GetInstance<ConsoleEventLogger>();
                var supplyBuilder = _container.GetInstance<SupplyBuilder>();
                supplyBuilder
                    .BasicGame()
                    .WithSet<FirstGame>()
                    .WithPlayers(2);
                var gameBuilder = _container.GetInstance<GameBuilder>();
                var gameSpec = new GameSpec().WithConsolePlayer("Bob").WithConsolePlayer("Ted");

                Game game = gameBuilder.Initialize(gameSpec);
                game.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
