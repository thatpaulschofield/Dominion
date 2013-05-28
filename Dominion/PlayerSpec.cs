namespace Dominion
{
    public class PlayerSpec
    {
        public PlayerType PlayerType { get; set; }

        public PlayerSpec WithPlayerType(PlayerType type)
        {
            PlayerType = type;
            return this;
        }

        public PlayerSpec WithPlayerName(string playerName)
        {
            PlayerName = playerName;
            return this;
        }

        public string PlayerName { get; set; }
    }

    public enum PlayerType
    {
        Console,
        AI
    }
}