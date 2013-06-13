using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;
using Dominion.Infrastructure;
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

        public Game(IEventAggregator eventAggregator, 
            ISupplyBuilder supplyBuilder, 
            IEnumerable<Player> players,
            IEnumerable<EndGameCondition> endGameConditions,
            IContainer container)
        {
            TurnNumber = 1;
            _eventAggregator = eventAggregator;
            _container = container;
            Players = players;
            Supply = supplyBuilder.BuildSupply();
            _playerIterator = Players.GetEnumerator();

            endGameConditions.ForEach(c => _endGameConditions.Add(c));

        }

        public Game(GameScope gameScope, IEnumerable<Player> players)
            : this(gameScope.EventAggregator, gameScope.SupplyBuilder, players, gameScope.EndGameConditions, gameScope.Container)
        {
            
        }


        public Supply Supply { get; private set; }

        public IEnumerable<Player> Players
        {
            get { return _players; }
            private set { _players = value.ToList(); }
        }

        public GameScore Start()
        {
            do
            {
                Player player = MoveToNextPlayer();
                PlayTurn(player);
                if (GameOver)
                    break;
            } while (!GameOver);
            return GameEnd();
        }

        private GameScore GameEnd()
        {
            var scores = new GameScore();
            var scope = new EndGameScope();
            foreach (var player in Players)
            {
                player.EndGameCleanup(this);
                scores.Add(new PlayerScore(player, player.CountScore(this)));
            }
            _eventAggregator.Publish(new GameEndedEvent(scores, scope));
            return scores;
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
