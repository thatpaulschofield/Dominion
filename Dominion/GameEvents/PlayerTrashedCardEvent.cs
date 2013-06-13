using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class PlayerTrashedCardEvent : GameMessage
    {
        public PlayerTrashedCardEvent(IActionScope scope, Card trashedCard) : base(scope)
        {
            Description = scope.Player.Name + "trashed a " + trashedCard.Name + ".";
        }
    }
}
