using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public class ResponseVotes
    {
        IList<ResponseScore> _votes = new List<ResponseScore>();

        public IEventResponse Winner { get
        {
            var winningScope = _votes.OrderByDescending(v => v.Score).FirstOrDefault();
            return winningScope.Response;
        } }

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
            var v1Votes = v1 == null || v1._votes == null 
                ? new ResponseScore[]{}
                : v1._votes.Where(v => v != null);
            var v2Votes = v2 == null || v2._votes == null
                ? new ResponseScore[] { }
                : v2._votes.Where(v => v != null);
            
            var combinedVotes = v1Votes.Union(v2Votes)
                                       .GroupBy(x => x.Response)
                                       .Select(z => new ResponseScore(z.Sum(y => y.Score), z.Key)).ToList();

            var sum = new ResponseVotes { _votes = combinedVotes };
            return sum;
        }
    }
}