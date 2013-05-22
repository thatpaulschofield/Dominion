using Dominion.Cards.BasicSet;
using Dominion.GameEvents;

namespace Dominion
{
    public class Turn
    {
        private readonly Player _player;
        private readonly TurnScope _scope;

        public Turn(Player player, TurnScope scope)
        {
            _player = player;
            _scope = scope;
        }

        public void Begin()
        {
            _player.BeginActionPhase(new ActionPhase(_scope, new CardSet()));
            _player.BeginBuyPhase(new BuyPhase(_scope, new CardSet(Action.Village)));
            _player.BeginCleanupPhase(_scope);
        }
    }
}