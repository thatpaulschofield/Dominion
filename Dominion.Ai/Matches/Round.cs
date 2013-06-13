using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Ai.Populations;
using Dominion.Cards;

namespace Dominion.Ai.Matches
{
    public class Round
    {
        private readonly int _seriesSize;
        private readonly int _gameSize;
        private readonly Population _competitors;
        private readonly GameBuilder _gameBuilder;
        private readonly Func<GameScope> _gameScopeFactory;
        private readonly AiPlayerBuilder _aiPlayerBuilder;

        public Round(int seriesSize, int gameSize, Population competitors, GameBuilder gameBuilder, Func<GameScope> gameScopeFactory)
        {
            _seriesSize = seriesSize;
            _gameSize = gameSize;
            _competitors = competitors;
            _gameBuilder = gameBuilder;
            _gameScopeFactory = gameScopeFactory;
        }

        public RoundResults Start()
        {
            var winners = new Population();

            var competitors = new Population(_competitors);

            var gameScores = new SeriesResults();
            do
            {
                var playersInCurrentGame = new Population();
                var game = _gameBuilder.Initialize(MakeGameSpec(competitors, playersInCurrentGame), _gameScopeFactory());
                var gameResult = game.Start();
                gameScores.Add(gameResult);
                var winner = playersInCurrentGame.Single(c => c.Id.ToString() == gameResult.WinningPlayer.Id.ToString());
                winners.Add(winner);
            } while (competitors.Count > 0);

            return new RoundResults
                {
                    Winners = winners,
                    Scores = gameScores
                };
        }

        private GameSpec MakeGameSpec(Population competitors, Population playersInCurrentGame)
        {
            var spec = new GameSpec().BasicGame().WithSet<FirstGame>();
            _gameSize.Times(() =>
                {
                    if (competitors.Any())
                    {
                        var competitor = competitors.First();
                        competitors.Remove(competitor);
                        playersInCurrentGame.Add(competitor);
                        var controller = new AiPlayerController(competitor);
                        var playerSpec = new PlayerSpec().WithController(controller).WithId(competitor.Id);
                        spec.WithPlayer(playerSpec);
                        
                    }
                });
            return spec;
        }
    }


}