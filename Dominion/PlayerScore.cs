namespace Dominion
{
    internal class PlayerScore
    {
        public PlayerScore(Player player, int countScore)
        {
            this.Player = player.Name;
            this.Score = countScore;
        }

        public int Score { get; private set; }

        public string Player { get; private set; }

        public override string ToString()
        {
            return Player + ": " + Score + " victory points";
        }
    }
}