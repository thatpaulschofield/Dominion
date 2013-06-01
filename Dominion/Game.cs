using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.VictoryCards;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion
{
    public class Game
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IContainer _container;
        private IEnumerator<Player> _playerIterator;
        private List<Player> _players;
        private readonly List<EndGameCondition> _endGameConditions = new List<EndGameCondition>();
        private TrashPile _trash = new TrashPile();

        public Game(IEventAggregator eventAggregator, Supply supply, IEnumerable<Player> players) : this(eventAggregator, supply, players, new Container())
        {
        }

        public Game(IEventAggregator eventAggregator, Supply supply, IEnumerable<Player> players, IContainer container)
        {
            TurnNumber = 1;
            _eventAggregator = eventAggregator;
            _container = container;
            Supply = supply;
            Players = players;
            _playerIterator = Players.GetEnumerator();
            _endGameConditions.Add(new ProvinceStackDepletedEndGameCondition(eventAggregator));
            _endGameConditions.Add(new ThreeSupplyPilesDepletedEndGameCondition(eventAggregator));

        }

        public static Deck DealStartupDeck()
        {
            return new Deck(7.Coppers(), 3.Estates()).Shuffle();
        }

        public Supply Supply { get; private set; }

        public IEnumerable<Player> Players
        {
            get { return _players; }
            private set { _players = value.ToList(); }
        }

        public void Start()
        {
            do
            {
                Player player = MoveToNextPlayer();
                PlayTurn(player);
                if (GameOver)
                    break;
            } while (!GameOver);
            GameEnd();
        }

        private void GameEnd()
        {
            var scores = new List<PlayerScore>();
            var scope = new EndGameScope();
            foreach (var player in Players)
            {
                player.EndGameCleanup(scope);
                scores.Add(new PlayerScore(player, player.CountScore(scope)));
            }
            _eventAggregator.Publish(new GameEndedEvent(scores, scope));
        }

        public int TurnNumber { get; private set; }

        private Player MoveToNextPlayer()
        {
            if (!_playerIterator.MoveNext())
            {
                TurnNumber++;
                _playerIterator = Players.GetEnumerator();
                _playerIterator.MoveNext();
            }
            return _playerIterator.Current;
        }

        private void PlayTurn(Player player)
        {
            using (player.BeginTurn(this))
            {}
        }

        protected bool GameOver
        {
            get { return _endGameConditions.Any(c => c.ConditionMet); }
        }

        public TurnScope StartTurn(Player player)
        {
            return new TurnScope(player, Supply, TurnNumber, _eventAggregator, PassivePlayers(player), _trash, _container.GetNestedContainer());
        }

        private IEnumerable<Player> PassivePlayers(Player player)
        {
            return Players.Where(p => !ReferenceEquals(p, player));
        }
    }
}
