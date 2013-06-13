using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Infrastructure;
using Dominion.PlayerControllers;
using Dominion.PlayerControllers.Console;
using StructureMap;

namespace Dominion
{
    public class GameBuilder
    {
        public Game Initialize(GameSpec spec, GameScope scope)
        {
            scope.SupplyBuilder.WithGameSpec(spec);
            return new Game(scope, BuildPlayers(scope, spec));
        }

        private IEnumerable<Player> BuildPlayers(GameScope gameScope, GameSpec spec)
        {
            return spec.Players
                .Select(playerSpec => gameScope.PlayerBuilder.ForSpec(playerSpec))
                .Select(dummy => (Player) dummy).ToList();
        }
    }
}