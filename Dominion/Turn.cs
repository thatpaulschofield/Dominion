using Dominion.GameEvents;

namespace Dominion
{
    public class Turn
    {
        private readonly Player _player;
        private readonly ITurnScope _scope;

        public Turn(Player player, ITurnScope scope)
        {
            _player = player;
            _scope = scope;
        }

        public void Begin()
        {
            if (_scope.TurnNumber == 1)
            {
                _player.DrawNewHand(_scope);
            }
            while (_scope.Actions > 0)
            {
                _player.BeginActionPhase(new ActionPhase(_scope, _player.Hand.Actions()));
            }
            while (_scope.Buys > 0)
            {
                _player.BeginBuyPhase(new BuyPhase(_scope));
            }
            _player.BeginCleanupPhase(_scope);
        }
    }
}