using System;
using System.Collections.Generic;
using Dominion.PlayerControllers;
using Dominion.PlayerControllers.Console;
using StructureMap;

namespace Dominion
{
    public class GameBuilder
    {
        private readonly ISupplyBuilder _supplyBuilder;
        private readonly IEventAggregator _eventAggregator;
        private PlayerBuilder _playerBuilder;
        private readonly DeckBuilder _deckBuilder;
        private readonly IEnumerable<EndGameCondition> _endGameConditions;
        private readonly IContainer _container;

        public GameBuilder(ISupplyBuilder supplyBuilder,
            IEventAggregator eventAggregator,
            PlayerBuilder playerBuilder,
            DeckBuilder deckBuilder) : this(supplyBuilder, eventAggregator, playerBuilder, deckBuilder, new List<EndGameCondition>(), new Container() )
        {
        }

        public GameBuilder(ISupplyBuilder supplyBuilder, 
            IEventAggregator eventAggregator, 
            PlayerBuilder playerBuilder,
            DeckBuilder deckBuilder, 
            IEnumerable<EndGameCondition> endGameConditions,
            IContainer container)
        {
            _supplyBuilder = supplyBuilder;
            _eventAggregator = eventAggregator;
            _playerBuilder = playerBuilder;
            _deckBuilder = deckBuilder;
            _endGameConditions = endGameConditions;
            _container = container;
            _supplyBuilder = supplyBuilder;
        }

        public Game Initialize(int players)
        {
            _supplyBuilder.WithPlayers(players);

            return new Game(_eventAggregator, _supplyBuilder, BuildPlayers(players), _endGameConditions, _container);            
        }

        public Game Initialize(GameSpec spec)
        {
            return new Game(_eventAggregator, _supplyBuilder, BuildPlayers(spec), _endGameConditions, _container);
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
                    default:
                        controller = new ConsolePlayerController();
                        break;
                    //case PlayerType.AI:
                    //    default:
                    //    //controller = new AiPlayerController(playerSpec.Ai);
                    //    break;
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
                players.Add(new Player(_deckBuilder.Build(), new DiscardPile(), new NaivePlayerController()));
            }
            return players;
        }


        public Deck DealStartupDeck()
        {
            return _deckBuilder.Build();
        }
    }
}