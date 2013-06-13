using System.Threading.Tasks;

namespace Dominion.Ai.Matches
{
    public class Match
    {
        private readonly Game _game;

        public Match(Game game)
        {
            _game = game;
        }

        public Task<GameScore> Start()
        {
            return new Task<GameScore>(() => _game.Start());
        }
    }


    //new Tournament(seriesSize, gameSize, population, null, null, null);
}
