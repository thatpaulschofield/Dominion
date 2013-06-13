using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominion.Ai.Matches
{
    public class Series
    {
        private readonly Func<Game> _gameBuilder;
        private int _rounds;

        public Series(IEnumerable<Game> games)
        {
            var enumerator = games.GetEnumerator();
            _gameBuilder = () =>
                {
                    enumerator.MoveNext();
                    return enumerator.Current;
                };
        }

        public Series(Func<Game> gameBuilder)
        {
            _gameBuilder = gameBuilder;
        }

        public Series WithLength(int rounds)
        {
            _rounds = rounds;
            return this;
        }

        public SeriesResults Start()
        {
            var matches = new List<Task<GameScore>>();
            _rounds.Times(() => StartMatch(matches));
            Task.WaitAll(matches.Where(m => m != null).ToArray());
            var seriesResults = new SeriesResults();
            seriesResults.AddRange(matches.Select(m => m.Result));
            return seriesResults;
        }

        private void StartMatch(List<Task<GameScore>> matches)
        {
            var match = new Match(_gameBuilder());
            var task = match.Start();
            matches.Add(task);
            task.Start();
        }
    }
}