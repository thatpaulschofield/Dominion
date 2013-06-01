using System;
using System.Linq;
using Dominion;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class MinePlayedMessage : GameMessage
    {
        public MinePlayedMessage(IActionScope scope) : base(scope)
        {
            Description = String.Format("{0} played Mine", scope.Player.Name);
        }
    }
}