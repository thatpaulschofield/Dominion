using System;
using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion
{
    public class Turn : IDisposable
    {
        private readonly Player _player;
        private readonly ITurnScope _scope;
        private readonly List<ReactionScope> _reactionScopes = new List<ReactionScope>();
        
        public Turn(Player player, ITurnScope scope)
        {
            _player = player;
            _scope = scope;
        }

        public void Begin()
        {
            EnsureFirstHandIsDrawn();
            ActionPhase();
            BuyPhase();
            CleanupPhase();
        }

        private void CleanupPhase()
        {
            _player.BeginCleanupPhase(_scope);
        }

        private void BuyPhase()
        {
            PlayTreasures();
            while (_scope.Buys > 0)
            {
                _player.BeginBuyPhase(new BuyPhase(_scope));
            }
        }

        private void PlayTreasures()
        {
            _player.HandleCommand(new SelectTreasuresToPlayCommand(_scope)).Execute();
        }

        private void ActionPhase()
        {
            while (_scope.Actions > 0)
            {
                _player.BeginActionPhase(new ActionPhase(_scope, _player.Hand.Actions()));
            }
        }

        private void EnsureFirstHandIsDrawn()
        {
            if (_scope.TurnNumber == 1)
            {
                _player.DrawNewHand(_scope);
            }
        }

        public void Dispose()
        {
            _scope.Dispose();
            _reactionScopes.ForEach(p => p.Dispose());
        }
    }
}