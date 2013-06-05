using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class SupplyBuilder : ISupplyBuilder
    {
        private readonly IEventAggregator _eventAggregator;
        private List<SupplyPile> _types = new List<SupplyPile>();
        private int _playerCount;
        private bool _basicGame;
        private DeckSet _set;

        public SupplyBuilder(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public ISupplyBuilder With(IEnumerable<Card> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
            var group = cards.ToList();
            if (group.Any())
                _types.Add(new SupplyPile(@group.Count, @group[0].CardType, _eventAggregator));

            return this;
        }

        public ISupplyBuilder WithPlayers(int playercount)
        {
            _playerCount = playercount;
            return this;
        }

        public ISupplyBuilder WithSet<T>() where T: DeckSet, new()
        {
            _set = new T();
            return this;
        }

        public ISupplyBuilder BasicGame()
        {
            _basicGame = true;
            return this;
        }

        public Supply BuildSupply()
        {
            if (_basicGame)
            {
                int victoryCards = (_playerCount == 1 || _playerCount == 2) ? 12 : 8;
                int curseCards = (_playerCount - 1) * 10;

                With(victoryCards.Estates());
                With(victoryCards.Duchies());
                With(victoryCards.Provinces());
                With(curseCards.Curses());
                With((60 - _playerCount*7).Coppers());
                With(40.Silvers());
                With(30.Golds());
            }
            if (_set != null)
            {
                _set.Build(this);
            }
            return new Supply(_types.ToArray());
        }

        public static implicit operator Supply(SupplyBuilder builder)
        {
            return builder.BuildSupply();
        }
    }
}