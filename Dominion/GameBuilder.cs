using System.Collections.Generic;
using Dominion.PlayerControllers;

namespace Dominion
{
    public class GameBuilder
    {
        private readonly SupplyBuilder _supplyBuilder;
        private readonly IEventAggregator _eventAggregator;
        private PlayerBuilder _playerBuilder;

        public GameBuilder(SupplyBuilder supplyBuilder, IEventAggregator eventAggregator, PlayerBuilder playerBuilder)
        {
            _supplyBuilder = supplyBuilder;
            _eventAggregator = eventAggregator;
            _playerBuilder = playerBuilder;
        }

        public Game Initialize(int players)
        {

            Supply supply = _supplyBuilder;
            //new Supply(new SupplyPile(victoryCards, Victory.Estate),
            //                    new SupplyPile(victoryCards, Victory.Duchy),
            //                    new SupplyPile(victoryCards, Victory.Province),
            //                    new SupplyPile(curseCards, BasicCards.Curse)
            //);


            return new Game(_eventAggregator, supply, BuildPlayers(players))
                {
                };            
        }

        public Game Initialize(GameSpec spec)
        {
            return new Game(_eventAggregator, _supplyBuilder, BuildPlayers(spec));
        }

        private IEnumerable<Player> BuildPlayers(GameSpec spec)
        {
            var players = new List<Player>();
            foreach (PlayerSpec playerSpec in spec.Players)
            {
                IPlayerController controller;
                switch (playerSpec.PlayerType)
                {
                    case PlayerType.Console:
                        controller = new ConsolePlayerController();
                        break;
                    case PlayerType.AI:
                        default:
                        controller = new AiPlayerController();
                        break;
                }
                players.Add(_playerBuilder.WithController(controller).WithName(playerSpec.PlayerName));
            }
            return players;
        }

        private IEnumerable<Player> BuildPlayers(int playerCount)
        {
            var players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(Game.DealStartupDeck(), new DiscardPile(), new NaivePlayerController(), _eventAggregator));
            }
            return players;
        }
    }
}