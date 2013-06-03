using Dominion.GameEvents;

namespace Dominion.AI
{
    public class ResponseScore
    {
        public ResponseScore(int score, IEventResponse response)
        {
            Score = score;
            Response = response;
        }
        public int Score { get; private set; }
        public IEventResponse Response { get; private set; }

        public void IncrementScore(int count)
        {
            Score += count;
        }
    }
}