using System;
using Dominion.Ai.Populations;

namespace Dominion.Ai.Matches
{
    public class TournamentBuilder : ITournamentBuilder
    {
        private readonly GameBuilder _gameBuilder;
        private readonly Func<GameScope> _gameScopeFactory;

        public TournamentBuilder(GameBuilder gameBuilder, Func<GameScope> gameScopeFactory)
        {
            _gameBuilder = gameBuilder;
            _gameScopeFactory = gameScopeFactory;
        }

        public Tournament Build(int seriesSize, int gameSize, Population population)
        {
            return new Tournament(seriesSize, gameSize, population, _gameBuilder, _gameScopeFactory);
        }
    }
}