using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class TreasurePickedToUpgradeToWithMine : GameEventResponse<PickTreasureToUpgradeToForMineCommand>
    {
        private readonly Card _card;

        public TreasurePickedToUpgradeToWithMine(Card card, ITurnScope scope) : base(scope)
        {
            _card = card;
            Description = String.Format("Upgrade to {0}", card.Name);
        }

        public override void Execute()
        {
            TurnScope.GainCardFromSupply(_card);
        }

        public Card CardToUpgradeTo { get { return _card; } }
    }
}