using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class MilitiaAttackEffect : GameMessage, IAttackEffect
    {
        public MilitiaAttackEffect(ITurnScope turnScope) : base(turnScope)
        {
            
        }


        public void Resolve(IReactionScope scope)
        {
            while (scope.ReceivingPlayer.Hand.Count() > 3)
            {
                scope.ReceivingPlayer.Handle(new DiscardCommand(scope, "You must discard down to 3 cards [Militia]."), scope);
            }
        }
    }
}