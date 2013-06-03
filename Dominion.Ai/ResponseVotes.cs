using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public class ResponseVotes
    {
        IList<ResponseScore> _votes = new List<ResponseScore>();

        public IEventResponse Winner { get { return _votes.OrderByDescending(v => v.Score).FirstOrDefault().Response; } }

        public ResponseVotes VoteFor(IEventResponse response, int count)
        {
            var clone = this.Clone();
            clone.CastVote(response, count);
            return clone;
        }

        private void CastVote(IEventResponse response, int count)
        {
            var score = _votes.FirstOrDefault(v => v.Response == response);
            if (score == null)
            {
                score = new ResponseScore(0, response);
                _votes.Add(score);
            }
            score.IncrementScore(count);
        }

        private ResponseVotes Clone()
        {
            return new ResponseVotes { _votes = new List<ResponseScore>(this._votes) };
        }

        public static ResponseVotes operator +(ResponseVotes v1, ResponseVotes v2)
        {
            var sum = new ResponseVotes
                {
                    _votes = v1._votes.Union(v2._votes)
                               .GroupBy(x => x.Response)
                               .Select(z => new ResponseScore(z.Sum(y => y.Score), z.Key)).ToList()
                };
            return sum;
        }
    }
}