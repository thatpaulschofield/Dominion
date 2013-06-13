using System;
using System.Collections.Generic;
using Dominion.Ai.Populations;

namespace Dominion.Ai.Matches
{
    public class Tournament
    {
        private readonly int _seriesSize;
        private readonly int _gameSize;
        private readonly Population _population;
        private readonly GameBuilder _gameBuilder;
        private readonly Func<GameScope> _gameScopeFactory;
        private readonly List<RoundResults> _roundResults = new List<RoundResults>();

        public Tournament(int seriesSize, int gameSize, Population population, GameBuilder gameBuilder, Func<GameScope> gameScopeFactory)
        {
            _seriesSize = seriesSize;
            _gameSize = gameSize;
            _population = population;
            _gameBuilder = gameBuilder;
            _gameScopeFactory = gameScopeFactory;
        }

        public void Begin()
        {
            var nextRound = new Population(_population);
            while (nextRound.Count > 1)
            {
                var round = new Round(_seriesSize, _gameSize, nextRound, _gameBuilder, _gameScopeFactory);
                var roundResult = round.Start();
                _roundResults.Add(roundResult);
                nextRound = roundResult.Winners;
            }
        }

        public TournamentResults GetResults()
        {
            return new TournamentResults(_roundResults);
        }
    }
}