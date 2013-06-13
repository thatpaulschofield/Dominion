
using System;

namespace Dominion
{
    public class PlayerSpec
    {
        public PlayerSpec()
        {
            this.PlayerId = new Player.PlayerId();
        }

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

        public PlayerSpec WithController(IPlayerController controller)
        {
            Controller = controller;
            return this;
        }

        public PlayerSpec WithId(Player.PlayerId id)
        {
            PlayerId = id;
            return this;
        }

        public string PlayerName { get; set; }

        public Player.PlayerId PlayerId { get; set; }

        public IPlayerController Controller { get; set; }
    }

    public class PlayerSpec<TCONTROLLER> : PlayerSpec where TCONTROLLER : IPlayerController
    {

        public PlayerSpec(TCONTROLLER controller)
        {
            Controller = controller;
        }
    }

    public enum PlayerType
    {
        Console,
        AI
    }
}