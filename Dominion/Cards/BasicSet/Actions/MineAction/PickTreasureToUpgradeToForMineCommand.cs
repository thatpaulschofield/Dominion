using System;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class PickTreasureToUpgradeToForMineCommand : GameMessage, IPlayerScoped
    {
        public PickTreasureToUpgradeToForMineCommand(Money cost, ITurnScope scope) : base(scope)
        {
            Description = String.Format("Pick a treasure costing {0} to upgrade to [Mine]", cost);
            GetAvailableResponses =
                () =>
                scope.Supply.FindCardsCostingUpTo(cost)
                     .Treasures().OrderByDescending(t => t.Cost)
                     .Select(t => new TreasurePickedToUpgradeToWithMine(t, scope));
        }
    }
}