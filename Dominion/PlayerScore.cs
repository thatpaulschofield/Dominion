namespace Dominion
{
    public class PlayerScore
    {
        public PlayerScore(Player player, int countScore)
        {
            this.Player = player;
            this.PlayerName = player.Name;
            this.Score = countScore;
        }

        public int Score { get; private set; }
        public Player Player { get; set; }
        public string PlayerName { get; private set; }

        public override string ToString()
        {
            return "    " + PlayerName + ": " + Score + " victory points";
        }
    }
}